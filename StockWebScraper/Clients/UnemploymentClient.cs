using HtmlAgilityPack;
using Nancy.Json;
using Newtonsoft.Json;
using StockWebScraper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockWebScraper.Clients
{
    public class UnemploymentClient
    {
        private readonly HttpClient httpClient;
        private string urlToNewsReleases = "https://www.bls.gov/bls/news-release/empsit.htm";

        public UnemploymentClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;

            //this.httpClient.BaseAddress = new Uri("http://localhost:57863");
            this.httpClient.Timeout = new TimeSpan(0, 0, 30);
            this.httpClient.DefaultRequestHeaders.Clear();
            
        }


        public async Task<Root> GetEmploymentSituationArchivedNewsReleases(CancellationToken cancellationToken)
        {

            var movieToUpdate = new SeriesPost()
            {
                seriesid = (new List<string>() { "LNS13000000" }).ToArray(),
                startyear = "1985",
                endyear = "2021",
                registrationKey = "8de17883726642c491b06560e17fa459"
            };

            var serializedMovieToUpdate = JsonConvert.SerializeObject(movieToUpdate);

            var request = new HttpRequestMessage(HttpMethod.Post,
                "https://api.bls.gov/publicAPI/v2/timeseries/data/");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Content = new StringContent(serializedMovieToUpdate);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await this.httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();
            var unemploymentData = stream.ReadAndDeserializeFromJson<Root>();

            return unemploymentData;
          
        }

    }
   
}
