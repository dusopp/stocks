using Microsoft.Extensions.Logging;
using StockWebScraper.Clients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockWebScraper.Services
{
    public class UnemploymentService : IService
    {
        private readonly UnemploymentClient unemploymentClient;
        private readonly ILogger logger;
        private readonly CancellationTokenSource _cancellationTokenSource =
            new CancellationTokenSource();

        public UnemploymentService(UnemploymentClient unemploymentClient, ILogger<UnemploymentService> logger)
        {
            this.unemploymentClient = unemploymentClient;
            this.logger = logger;
        }

        public async Task Run()
        {
            var unemploymentData = await unemploymentClient.Get(_cancellationTokenSource.Token);

            //logger.LogDebug(test);
        }
    }
}
