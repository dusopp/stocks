using System;
using System.Collections.Generic;
using System.Text;

namespace StockWebScraper.Models
{
    public class SeriesPost
    {
        public string[] seriesid { get; set; }
        public string startyear { get; set; }
        public string endyear { get; set; }       
        public string registrationKey { get; set; }
    }
}
