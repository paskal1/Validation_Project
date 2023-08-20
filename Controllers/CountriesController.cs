using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Validation_Project.Controllers
{
    /// <summary>
    /// Represents a set of endpoints to retrieve information about contries.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CountriesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<ActionResult> Get(
            [FromQuery] string? param1 = null,
            [FromQuery] int? param2 = null,
            [FromQuery] string? param3 = null,
            [FromQuery] string? param4 = null)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(Constants.RestCountries.HttpClientName);
                var response = await httpClient.GetAsync(Constants.RestCountries.All);

                if (response.IsSuccessStatusCode)
                {
                    string countriesJson = await response.Content.ReadAsStringAsync();
                    return Ok(countriesJson);
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        private static List<Country> FilterCountries(List<Country> countries, string searchString)
        {
            string searchLower = searchString.ToLower(CultureInfo.InvariantCulture);
            return countries
                .Where(country =>
                    country.Name.ToLower(CultureInfo.InvariantCulture).Contains(searchLower) ||
                    country.CommonName.ToLower(CultureInfo.InvariantCulture).Contains(searchLower))
                .ToList();
        }
    }
}
