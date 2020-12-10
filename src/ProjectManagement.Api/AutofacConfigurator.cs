using Autofac;
using Microsoft.Extensions.Configuration;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Infrastructure;
using ProjectManagement.Api.Repository;
using ProjectManagement.Api.Services;
using ProjectManagement.Api.Services.Validator;

namespace ProjectManagement.Api
{
    public static class AutofacConfigurator 
    {
        public static void Configure(IConfiguration configuration, ContainerBuilder builder)
        {
            builder.RegisterType<ProjectDao>().As<IProjectDao>().SingleInstance();
            builder.RegisterType<EmployeeDao>().As<IEmployeeDao>().SingleInstance();
            builder.RegisterType<EmployeeRoleDao>().As<IEmployeeRoleDao>().SingleInstance();
            builder.RegisterType<DepartmentDao>().As<IDepartmentDao>().SingleInstance();
            
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();

            builder.RegisterType<ProjectValidator>();
            
            builder.RegisterInstance(configuration).As<IConfiguration>();

            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().SingleInstance();

            builder.RegisterType<ProjectFactory>().As<IProjectFactory>();
            builder.RegisterType<EmployeeFactory>().As<IEmployeeFactory>();
        }
    }
}