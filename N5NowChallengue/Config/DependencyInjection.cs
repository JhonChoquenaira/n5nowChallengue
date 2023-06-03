using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using N5NowChallengue.BusinessService;
using N5NowChallengue.BusinessService.Interfaces;
using N5NowChallengue.DataService;
using N5NowChallengue.DataService.Context;
using N5NowChallengue.DataService.Interface;
using N5NowChallengue.DataService.Repositories;

namespace N5NowChallengue.Config
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services, IConfiguration configuration)
        {
            //DataServices
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IPermissionRepository, PermissionRepository>();
            services.AddTransient<IEmployeePermissionRepository, EmployeePermissionRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            
            //BusinessServices
            services.AddTransient<IPermissionBusinessService, PermissionBusinessService>();
            services.AddTransient<IEmployeePermissionsBusinessService, EmployeePermissionBusinessService>();

            var appSettings = configuration.Get<AppSettings>();
            services.AddDbContext<ApplicationDbContext>(opt => opt
                .UseSqlServer(appSettings.Settings.DatabaseConnection));
            return services;
        }
    }
}
