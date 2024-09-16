//using AspNetCoreRateLimit;
//using AuthPlus.Identity.Dtos;
//using AuthPlus.Identity.Entities;
//using AuthPlus.Identity.Extensions;
//using AuthPlus.Identity.Helpers;
//using AuthPlus.Identity.Interfaces;
//using AuthPlus.Identity.Services;
//using AuthPlus.Identity.Validators;
//using GraphQLReact.API.GraphQL;
//using GraphQLReact.BLL.Helper;
//using GraphQLReact.BLL.Interfaces;
//using GraphQLReact.BLL;
//using GraphQLReact.DLL.Data;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Options;
//using Microsoft.OpenApi.Models;
//using System.Text.Json.Serialization;
//using System.Text.Json;

//namespace GraphQLReact.UI.Server.Extensions;

//public static class ServiceCollectionExtensions
//{
//    public static void ConfigureCustomServices(this IServiceCollection services, IConfiguration configuration)
//    {
//        services.AddControllers()
//            .AddJsonOptions(options =>
//            {
//                options.JsonSerializerOptions.MaxDepth = 64;
//                options.JsonSerializerOptions.IgnoreNullValues = true;
//                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
//                options.JsonSerializerOptions.WriteIndented = true;
//                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//            });

//        services.AddDbContext<ProductDbContext>(options =>
//            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

//        services.AddEndpointsApiExplorer();
//        services.AddSwaggerGen(c =>
//        {
//            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
//            {
//                Name = "Authorization",
//                Type = SecuritySchemeType.ApiKey,
//                Scheme = "Bearer",
//                BearerFormat = "JWT",
//                In = ParameterLocation.Header,
//                Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer {token}\"",
//            });

//            c.AddSecurityRequirement(new OpenApiSecurityRequirement
//            {
//                {
//                    new OpenApiSecurityScheme
//                    {
//                        Reference = new OpenApiReference
//                        {
//                            Type = ReferenceType.SecurityScheme,
//                            Id = "Bearer"
//                        }
//                    },
//                    new string[] { }
//                }
//            });
//        });

//        services.AddHttpClient<GoogleAuthProvider>();
//        services.AddHttpClient<MicrosoftAuthProvider>();
//        services.AddHttpClient<LinkedInAuthProvider>();

//        services.AddScoped<IAuthService, AuthService>();
//        services.AddScoped<IExternalAuthProvider, GoogleAuthProvider>();
//        services.AddScoped<IExternalAuthProvider, MicrosoftAuthProvider>();
//        services.AddScoped<IExternalAuthProvider, LinkedInAuthProvider>();

//        services.AddIdentity<ApplicationUser, ApplicationRole>()
//            .AddEntityFrameworkStores<ProductDbContext>()
//            .AddDefaultTokenProviders();

//        services.AddCors(options =>
//        {
//            options.AddPolicy("AllowAllOrigins", policy =>
//            {
//                policy.AllowAnyOrigin()
//                      .AllowAnyMethod()
//                      .AllowAnyHeader();
//            });
//        });

//        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
//        services.AddSingleton<IEmailService>(serviceProvider =>
//        {
//            var emailSettings = serviceProvider.GetRequiredService<IOptions<EmailSettings>>().Value;
//            return new EmailService(emailSettings.SmtpServer, emailSettings.SmtpPort, emailSettings.SmtpUser, emailSettings.SmtpPassword);
//        });

//        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
//        services.AddSingleton<JwtHelper>(serviceProvider =>
//        {
//            var jwtSettings = serviceProvider.GetRequiredService<IOptions<JwtSettings>>().Value;
//            return new JwtHelper(jwtSettings.SecretKey, jwtSettings.Issuer, jwtSettings.Audience);
//        });

//        services.AddTransient<IBaseValidator<LoginDto>, LoginDtoValidator>();
//        services.AddTransient<IBaseValidator<RegisterDto>, RegisterDtoValidator>();
//        services.AddTransient<IBaseValidator<ResetPasswordDto>, ResetPasswordDtoValidator>();
//        services.AddTransient<IBaseValidator<UserDto>, UserDtoValidator>();

//        services.AddScoped<IUserService, UserService>();
//        services.AddScoped<IRoleService, RoleService>();
//        services.AddScoped<IProductService, ProductService>();
//        services.AddScoped<ICategoryService, CategoryService>();

//        services.AddAutoMapper(typeof(MapperProfile));

//        services
//            .AddGraphQLServer()
//            .AddQueryType<Query>()
//            .AddMutationType<Mutation>()
//            .AddProjections()
//            .AddFiltering()
//            .AddSorting()
//            .AddQueryableCursorPagingProvider(defaultProvider: true)
//            .AddAuthorization(options =>
//            {
//                options.AddPolicy("RequireAdminRole", policy =>
//                    policy.RequireRole(RoleConstants.AdminRole));
//                options.AddPolicy("RequireUserRole", policy =>
//                    policy.RequireRole(RoleConstants.UserRole));
//            });

//        services.AddJwtAuthentication(configuration);

//        services.AddAuthorization(options =>
//        {
//            options.AddPolicy("RequireAdminRole", policy =>
//                policy.RequireRole(RoleConstants.AdminRole));
//            options.AddPolicy("RequireUserRole", policy =>
//                policy.RequireRole(RoleConstants.UserRole));
//        });

//        services.AddMemoryCache();

//        services.AddOptions();
//        services.AddMemoryCache(); // Ensure IMemoryCache is added
//        services.AddInMemoryRateLimiting();
//        services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
//        services.Configure<RateLimitOptions>(configuration.GetSection("RateLimitRules"));
//        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
//    }
//}