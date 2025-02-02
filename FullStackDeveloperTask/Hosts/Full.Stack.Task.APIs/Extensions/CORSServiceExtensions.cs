namespace Full.Stack.Task.APIs.Extensions
{
    public static class CORSServiceExtensions
    {
        public static IServiceCollection AddAPICORSExtension(this IServiceCollection services, IConfiguration config, string MyAllowSpecificOrigins)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins, configurePolicy: builder =>
                {
                    builder.WithOrigins(config.GetValue<string>("siteCors")!.Split(";"))
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
                });
            });
            return services;
        }
    }
}
