using Newtonsoft.Json;

namespace BackendNbpApi.Model
{
    public class NbpRateResponse
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("rates")]
        public List<NbpRate> Rates { get; set; }
    }
    public class NbpRate
    {
        [JsonProperty("no")]
        public string? No { get; set; }

        [JsonProperty("effectiveDate")]
        public DateTime EffectiveDate { get; set; }

        [JsonProperty("mid")]
        public decimal Mid { get; set; }

        [JsonProperty("bid")]
        public decimal Bid { get; set; }

        [JsonProperty("ask")]
        public decimal Ask { get; set; }
    }
}
