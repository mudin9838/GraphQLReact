namespace GraphQLReact.UI.Server.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void ConfigureCustomMiddleware(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors("AllowAllOrigins");
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            //endpoints.Map("/swagger/{**slug}", async context =>
            //{
            //    await context.Response.WriteAsync("Swagger UI");
            //}).AllowAnonymous();

            endpoints.MapControllers().RequireAuthorization();
            endpoints.MapGraphQL().RequireAuthorization();

            // Ensure fallback to the index.html file for SPA routing
            endpoints.MapFallbackToFile("/index.html");
        });
    }
}