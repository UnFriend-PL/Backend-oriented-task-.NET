using BackendNbpApi.Model;
using Newtonsoft.Json;

namespace BackendNbpApi.NbpApi
{
    public class NbpClient
    {
        private const string BaseUrl = "http://api.nbp.pl/api/";
        private readonly HttpClient _httpClient;

        public NbpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<decimal> GetAverageExchangeRateAsync(string currencyCode, DateTime date)
        {
            try
            {
                var dateString = date.ToString("yyy-MM-dd");
                var endpoint = $"exchangerates/rates/a/{currencyCode}/{dateString}?format=json";
                var response = await _httpClient.GetAsync(BaseUrl + endpoint);
                Console.WriteLine($"Request URL: {BaseUrl + endpoint}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException($"Błąd podczas pobierania danych z API NBP: {response.StatusCode}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<NbpRateResponse>(content);
                return result.Rates.First().Mid;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wyjątek podczas pobierania danych z API NBP: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Returns min and max value average
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<(decimal Max, decimal Min)> GetMinMaxAverageAsync(string currencyCode, int numberOfQuotations)
        {
            string endpoint = $"exchangerates/rates/a/{currencyCode}/last/{numberOfQuotations}/";
            var response = await _httpClient.GetAsync(BaseUrl + endpoint);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Błąd podczas pobierania danych z API NBP: {response.StatusCode}");
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var rateResponse = JsonConvert.DeserializeObject<NbpRateResponse>(jsonResponse);

            decimal max = rateResponse.Rates.Max(rate => rate.Mid);
            decimal min = rateResponse.Rates.Min(rate => rate.Mid);

            return (max, min);
        }

        public async Task<decimal> GetMajorDifferenceBetweenBuyAndAskRateAsync(string currencyCode, int numberOfQuotations)
        {
            string endpoint = $"exchangerates/rates/c/{currencyCode}/last/{numberOfQuotations}/";
            var response = await _httpClient.GetAsync(BaseUrl + endpoint);

            if (!response.IsSuccessStatusCode)
            {
                throw new InvalidOperationException($"Błąd podczas pobierania danych z API NBP: {response.StatusCode}");

            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var rateResponse = JsonConvert.DeserializeObject<NbpRateResponse>(jsonResponse);

            decimal majorDifference = rateResponse.Rates.Max(rate => rate.Ask - rate.Bid);

            return majorDifference;
        }
    }
}
