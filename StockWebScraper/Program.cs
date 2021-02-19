using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockWebScraper.Clients;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using StockWebScraper.Services;
using StockWebScraper.Common;

namespace StockWebScraper
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // create a new ServiceCollection 
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
           
            // create a new ServiceProvider
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // For demo purposes: overall catch-all to log any exception that might 
            // happen to the console & wait for key input afterwards so we can easily 
            // inspect the issue.  
            try
            {
                // Run our IntegrationService containing all samples and
                // await this call to ensure the application doesn't 
                // prematurely exit.
                //await serviceProvider.GetService<IIntegrationService>().Run();

                await serviceProvider.GetService<UnemploymentService>().Run();
                
            }
            catch (Exception generalException)
            {
                // log the exception
                var logger = serviceProvider.GetService<ILogger<Program>>();
                 logger.LogError(generalException,
                    "An exception happened while running the integration service.");                
            }

           // Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // add loggers           
            serviceCollection.AddSingleton(
                LoggerFactory.Create(builder => builder
                .AddConsole()
                .AddDebug()
                )
            );

            serviceCollection.AddLogging();
            

            serviceCollection
                .AddHttpClient<UnemploymentClient>()
                .AddHttpMessageHandler(handler => new TimeOutDelegatingHandler(TimeSpan.FromSeconds(20)))
                .AddHttpMessageHandler(handler => new RetryPolicyDelegatingHandler(2))
                .ConfigurePrimaryHttpMessageHandler(handler =>
                new HttpClientHandler()
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                });

            serviceCollection.AddScoped<UnemploymentService>();

           
        }

    }
}
