using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockWebScraper.Models
{
    class BlsGov
    {
    }

    public class ActualData
    {
        [JsonProperty("year")]
        public string Year { get; set; }

        [JsonProperty("period")]
        public string Period { get; set; }

        [JsonProperty("periodName")]
        public string PeriodName { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }       
    }

    public class Series
    {
        [JsonProperty("seriesID")]
        public string SeriesID { get; set; }

        [JsonProperty("data")]
        public List<ActualData> Data { get; set; }
    }

    public class Results
    {
        [JsonProperty("series")]
        public List<Series> Series { get; set; }
    }

    public class Root
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("responseTime")]
        public int ResponseTime { get; set; }

        [JsonProperty("message")]
        public List<string> Message { get; set; }

        [JsonProperty("Results")]
        public Results Results { get; set; }
    }
}
