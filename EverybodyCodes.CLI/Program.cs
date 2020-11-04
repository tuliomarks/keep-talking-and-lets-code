using EverybodyCodes.CLI.Commands;
using EverybodyCodes.Infrastructure.Extension;
using EverybodyCodes.Infrastructure.Extension.Interfaces;
using EverybodyCodes.Infrastructure.Repositories;
using EverybodyCodes.Infrastructure.Repositories.Interfaces;
using EverybodyCodes.Infrastructure.Services;
using EverybodyCodes.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Invocation;
using System.CommandLine.Parsing;
using System.IO;
using System.Threading.Tasks;

namespace EverybodyCodes.CLI
{
    class Program
    {

        static async Task Main(string[] args)
        {
            await BuildCommandLine()
            .UseHost(_ => Host.CreateDefaultBuilder(),
                host =>
                {
                    host.ConfigureServices(services =>
                    {
                        ConfigureServices(services);
                    });
                })
            .UseDefaults()
            .Build()
            .InvokeAsync(args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json")
                            .AddEnvironmentVariables()
                            .Build();


            var config = new RepositoryConfig();
            configuration.GetSection("RepositoryConfig").Bind(config);

            services.AddSingleton<RepositoryConfig>(config);
            services.AddSingleton<IDataContext, FileDataContext>();

            services.AddScoped<ICameraRepository, CameraRepository>();

            services.AddScoped<ICameraService, CameraService>();
        }

        private static CommandLineBuilder BuildCommandLine()
        {
            var searchCommand = new Command("Search")
                {
                    new Option<string>("--name"){
                        IsRequired = true
                    },
                };

            var root = new RootCommand(@"$ EverybodyCodes.CLI Search --name Neude")
            {
                searchCommand
            };
    
            searchCommand.Handler = CommandHandler.Create<SearchCommandArgs, IHost>(SearchHandle);
            return new CommandLineBuilder(root);
           
        }

        private static void SearchHandle(SearchCommandArgs options, IHost host)
        {

            var loggerFactory = host.Services.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(typeof(Program));

            if (!string.IsNullOrEmpty(options.Name))
            {
                var service = host.Services.GetRequiredService<ICameraService>();

                var cameras = service.GetByName(options.Name);
                foreach (var cam in cameras)
                {
                    var text = $"{cam.Id};{cam.Name};{cam.Latitude};{cam.Longitude};";
                    //logger.LogInformation(text);
                    Console.WriteLine(text);
                }
            }
        }

    }
}
