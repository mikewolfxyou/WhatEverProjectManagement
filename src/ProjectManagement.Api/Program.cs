using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ProjectManagement.Api
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var applicationTokenSource = new ApplicationWideCancellationTokenSource(new CancellationTokenSource());
            await CreateHostBuilder(args, applicationTokenSource).Build().RunAsync(applicationTokenSource.TokenSource.Token);
        }

        private static IHostBuilder CreateHostBuilder(string[] args, ApplicationWideCancellationTokenSource tokenSource) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureHostConfiguration(configurationBuilder =>
                {
                    configurationBuilder
                        .AddInMemoryCollection(new[]
                        {
                            new KeyValuePair<string, string>(
                                HostDefaults.EnvironmentKey,
                                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")),
                        })
                        .AddEnvironmentVariables();
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton(tokenSource);
                })
                .ConfigureContainer<ContainerBuilder>((context, builder) =>
                {
                    AutofacConfigurator.Configure(context.Configuration, builder);
                });
    }
}