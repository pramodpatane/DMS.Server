using Server.Application.Interfaces;
using Server.Application.Services;
using Server.Application.Services.Core;
using Server.Application.Services.Feature;
using Server.Infrastructure.DAL;
using Server.Infrastructure.DAL.Interfaces;
using Server.Infrastructure.Repositories;
using Server.Infrastructure.Repositories.Interfaces;

namespace DMS.Server
{
    public class RegisterServices
    {
        public static void Register(IServiceCollection services)
        {
            // Dependency Injection
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeDAL, EmployeeDAL>();
            services.AddScoped<IEmployeeRepository, EmplopyeeRepository>();

            services.AddScoped<IClientsService, ClientsService>();
            services.AddScoped<IClientsDAL, ClientsDAL>();

            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IUsersDAL, UsersDAL>();

            services.AddScoped<IUserRolesService, UserRolesService>();
            services.AddScoped<IUserRolesDAL, UserRolesDAL>();

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDepartmentDAL, DepartmentDAL>();

            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<ICollectionDAL, CollectionDAL>();

            // Email Services Dependencies
            services.AddTransient<IEmailDAL, EmailDAL>();
            services.AddTransient<IEmailService, EmailService>();

            services.AddTransient<IOtpDAL, OtpDAL>();
            services.AddTransient<IOTPService, OTPService>();

            // Auth/login services dependencies
            services.AddSingleton<IJWTTokenService, JWTTokenService>();

            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IAuthDAL, AuthDAL>();

            services.AddSingleton<IAppMenusService, AppMenusService>();
            services.AddSingleton<IAppMenusDAL, AppMenusDAL>();
        }
    }
}
