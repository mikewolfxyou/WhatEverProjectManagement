using Autofac;
using Microsoft.Extensions.Configuration;
using ProjectManagement.Api.DataAccess;
using ProjectManagement.Api.Repository;
using ProjectManagement.Api.Services;

namespace ProjectManagement.Api
{
    public static class AutofacConfigurator 
    {
        public static void Configure(IConfiguration configuration, ContainerBuilder builder)
        {
            builder.RegisterType<ProjectDao>().As<IProjectDao>().SingleInstance();
            builder.RegisterType<EmployeeDao>().As<IEmployeeDao>();
            builder.RegisterType<DepartmentDao>().As<IDepartmentDao>();
            
            builder.RegisterType<ProjectRepository>().As<IProjectRepository>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();

            builder.RegisterType<ProjectValidator>();
            
            builder.RegisterInstance(configuration).As<IConfiguration>();
        }
    }
}