using Full.Stack.Task.APIs.Extensions;
using Full.Stack.Task.Application.Extensions;
using Full.Stack.Task.Persistence.Extensions;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;
using System.Text.Json.Serialization;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        builder.Logging.ClearProviders().AddNLog("nlog.config");
        builder.Services.AddPersistenceServices(builder.Configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerDocumentation();
        builder.Services.AddAPICORSExtension(builder.Configuration, MyAllowSpecificOrigins);

        builder.Services.ConfigureJwt(builder.Configuration);
        builder.Services.AddAuthorization();
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Full Stack Task v1");
                c.RoutePrefix = "swagger";
            });
        }

        app.UseCors(MyAllowSpecificOrigins);
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        

        app.Run();
    }
}