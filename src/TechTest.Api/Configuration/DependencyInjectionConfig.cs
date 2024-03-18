using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using TechTest.Business.Interfaces;
using TechTest.Business.Notifier;
using TechTest.Business.Services;
using TechTest.Data.Context;
using TechTest.Data.Repository;

namespace TechTest.Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MyDbContext>();
            services.AddScoped<ICallDetailRecordRepository, CallDetailRecordRepository>();
            services.AddScoped<ICallDetailRecordService, CallDetailRecordService>();
            services.AddScoped<INotifier, Notifier>();
            return services;
        }
    }
}
