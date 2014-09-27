using System;
using Newtonsoft.Json;

namespace Minfin.mb
{
    public class CurrencyApiResponse
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "pointDate")]
        public DateTime PointDate { get; set; }

        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        [JsonProperty(PropertyName = "ask")]
        public decimal Ask { get; set; }

        [JsonProperty(PropertyName = "bid")]
        public decimal Bid { get; set; }

        [JsonProperty(PropertyName = "currency")]
        public string Currency { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
    }
}