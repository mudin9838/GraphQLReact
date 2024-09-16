using GraphQLReact.BLL.Interfaces;
using GraphQLReact.BLL.Helper;
using GraphQLReact.BLL;
using GraphQLReact.DLL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using AuthPlus.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using AuthPlus.Identity.Helpers;
using AuthPlus.Identity.Interfaces;
using AuthPlus.Identity.Services;
using AuthPlus.Identity.Extensions;
using AuthPlus.Identity.Validators;
using AuthPlus.Identity.Dtos;
using AspNetCoreRateLimit;
using Microsoft.OpenApi.Models;
using GraphQLReact.API.GraphQL;
using System.Text.Json.Serialization;
using System.Text.Json;
using GraphQLReact.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Add DbContext
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.MaxDepth = 32; // Set maximum depth for serialization
        options.JsonSerializerOptions.IgnoreNullValues = true; // Ignore null values
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; // Avoid circular references
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; // Use camelCase for property names
        options.JsonSerializerOptions.WriteIndented = true; // Pretty print JSON
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{

    // To enable authorization using swagger (Jwt)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer {token}\"",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
{
    new OpenApiSecurityScheme
        {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}

            }
        });

});


// Register external authentication providers
builder.Services.AddHttpClient<GoogleAuthProvider>();
builder.Services.AddHttpClient<MicrosoftAuthProvider>();
builder.Services.AddHttpClient<LinkedInAuthProvider>();

// AuthService and external providers
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IExternalAuthProvider, GoogleAuthProvider>();
builder.Services.AddScoped<IExternalAuthProvider, MicrosoftAuthProvider>();
builder.Services.AddScoped<IExternalAuthProvider, LinkedInAuthProvider>();

// Configure Identity with your custom DbContext
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ProductDbContext>()
    .AddDefaultTokenProviders();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configure EmailSettings
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Register EmailService with EmailSettings
builder.Services.AddSingleton<IEmailService>(serviceProvider =>
{
    var emailSettings = serviceProvider.GetRequiredService<IOptions<EmailSettings>>().Value;
    return new EmailService(emailSettings.SmtpServer, emailSettings.SmtpPort, emailSettings.SmtpUser, emailSettings.SmtpPassword);
});

// Configure JwtHelper
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.AddSingleton<JwtHelper>(serviceProvider =>
{
    var jwtSettings = serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value;
    return new JwtHelper(jwtSettings.SecretKey, jwtSettings.Issuer, jwtSettings.Audience);
});

// Register validators
builder.Services.AddTransient<IBaseValidator<LoginDto>, LoginDtoValidator>();
builder.Services.AddTransient<IBaseValidator<RegisterDto>, RegisterDtoValidator>();
builder.Services.AddTransient<IBaseValidator<ResetPasswordDto>, ResetPasswordDtoValidator>();
builder.Services.AddTransient<IBaseValidator<UserDto>, UserDtoValidator>();

// Register other services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddHttpContextAccessor(); // Register IHttpContextAccessor to get HttpContext and access user information:
builder.Services.AddScoped<IUserContextService, UserContextService>();
// Configure GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddQueryableCursorPagingProvider(defaultProvider: true)
    .AddAuthorization();


// Configure Jwt Authentication
builder.Services.AddJwtAuthentication(builder.Configuration);

// Add Authorization Policies
//builder.Services.AddAuthorizationPolicies();
builder.Services.AddAuthorizationPolicies();

// Register IMemoryCache
builder.Services.AddMemoryCache();

// Register rate limiting services
builder.Services.AddOptions();
builder.Services.AddMemoryCache(); // Ensure IMemoryCache is added
builder.Services.AddInMemoryRateLimiting();
// Configure Rate Limiting
builder.Services.AddInMemoryRateLimiting();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<RateLimitOptions>(builder.Configuration.GetSection("RateLimitRules"));
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Configure logging
builder.Logging.ClearProviders(); // Optional: clear default providers
builder.Logging.AddConsole(); // Add console logging
builder.Logging.AddDebug();   // Add debug logging

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
// Configure the HTTP request pipeline.


app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // Ensure Swagger UI is accessible without authorization
    //endpoints.Map("/swagger/{**slug}", async context =>
    //{
    //    await context.Response.WriteAsync("Swagger UI");
    //}).AllowAnonymous();

    // Map controllers with global authorization
    endpoints.MapControllers().RequireAuthorization("RequireAdminOrUserRole");



    // Ensure GraphQL is protected
    endpoints.MapGraphQL();
});

app.MapFallbackToFile("/index.html");

app.Run();
