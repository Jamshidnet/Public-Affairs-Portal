using Application;
using Infrustructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NewProject.Abstraction;
using NewProject.Middlewares;
using PublicAffairsPortal.WebUI.CustomAttributes;
using System;

namespace NewProject;

public class Program
{
    public static void Main(string[] args)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        var builder = WebApplication.CreateBuilder(args);
        _ = builder.Services.AddApplication();
        _ = builder.Services.AddApi(builder.Configuration);
        _ = builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationExceptionFilter)));
        _ = builder.Services.AddEndpointsApiExplorer();
        _ = builder.Services.AddSwaggerGen();
        _ = builder.Services.AddScoped<ChangeLoggingMiddleware>();
        _ = builder.Services.AddTransient<IApplicationDbContext, NewdatabaseContext>();
        _ = builder.Services.AddDbContext<NewdatabaseContext>(
            options =>
            {
                _ = options.UseNpgsql(builder.Configuration.GetConnectionString("dbconnect"));
                _ = options.UseLazyLoadingProxies();
            }
            ); ;
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(c =>
            {
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            });
            _ = app.UseSwaggerUI();

        }

        _ = app.UseHttpsRedirection();

        _ = app.UseAuthentication();
        _ = app.UseAuthorization();

        _ = app.UseMiddleware<ChangeLoggingMiddleware>();
        _ = app.MapControllers();

        app.Run();
    }
}


