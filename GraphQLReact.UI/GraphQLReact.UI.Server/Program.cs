using GraphQLReact.BLL.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;
using GraphQLReact.BLL.Helper;
using GraphQLReact.BLL;
using GraphQLReact.DLL.Data;
using Microsoft.EntityFrameworkCore;
using GraphQLReact.API.GraphQL;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Add DbContext
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.MaxDepth = 64; // Increase depth if needed
        options.JsonSerializerOptions.IgnoreNullValues = true;
        options.JsonSerializerOptions.ReferenceHandler = null;
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.WriteIndented = true; // Pretty print JSON
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Handle enums as strings
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});
// Register GenericService with specific types
// Register generic services with specific DTO and entity types
// Register generic services with specific DTO and entity types
// Register generic service
// Register other services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MapperProfile));






builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure GraphQL



// Configure GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddQueryableCursorPagingProvider(defaultProvider: true);
var app = builder.Build();
app.UseCors("AllowAllOrigins");
app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// Other middleware configurations
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL(); // Maps GraphQL endpoint
});
app.MapFallbackToFile("/index.html");


app.Run();
