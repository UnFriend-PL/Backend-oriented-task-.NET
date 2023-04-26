using BackendNbpApi.NbpApi;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BackendNbpApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        private readonly NbpClient _nbpClient;

        public CurrencyController(NbpClient nbpClient)
        {
            _nbpClient = nbpClient;
        }

        /// <summary>
        /// Gets the average exchange rate for a given currency and date.
        /// </summary>
        /// <param name="currencyCode">The currency code, e.g. "USD".</param>
        /// <param name="date">The date in format "yyyy-MM-dd".</param>
        [HttpGet("average-exchange-rate")]
        public async Task<ActionResult<decimal>> GetAverageExchangeRate([FromQuery] string currencyCode, [FromQuery] DateTime date)
        {
            try
            {
                var result = await _nbpClient.GetAverageExchangeRateAsync(currencyCode, date);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Gets the minimum and maximum average exchange rate for a given currency and the last N quotations.
        /// </summary>
        /// <param name="currencyCode">The currency code, e.g. "USD".</param>
        /// <param name="numberOfQuotations">The number of last quotations (N <= 255).</param>
        [HttpGet("min-max-average")]
        public async Task<ActionResult<(decimal Max, decimal Min)>> GetMinMaxAverage([FromQuery] string currencyCode, [FromQuery] int numberOfQuotations)
        {
            try
            {
                var (max, min)  = await _nbpClient.GetMinMaxAverageAsync(currencyCode, numberOfQuotations);
                var result = JsonConvert.SerializeObject(new { min = min, max= max}, Formatting.Indented);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Gets the major difference between the buy and ask rate for a given currency and the last N quotations.
        /// </summary>
        /// <param name="currencyCode">The currency code, e.g. "USD".</param>
        /// <param name="numberOfQuotations">The number of last quotations (N <= 255).</param>
        [HttpGet("major-difference")]
        public async Task<ActionResult<decimal>> GetMajorDifferenceBetweenBuyAndAskRate([FromQuery] string currencyCode, [FromQuery] int numberOfQuotations)
        {
            try
            {
                var result = await _nbpClient.GetMajorDifferenceBetweenBuyAndAskRateAsync(currencyCode, numberOfQuotations);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}