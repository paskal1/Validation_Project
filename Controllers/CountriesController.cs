using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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
        public async Task<ActionResult<List<Country>>> Get([FromQuery] string? country = null, [FromQuery] int? maxPopulation = null, [FromQuery] string? sortOrder = null, [FromQuery] int? limit = null)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient(Constants.RestCountries.HttpClientName);
                var response = await httpClient.GetAsync(Constants.RestCountries.All);

                if (response.IsSuccessStatusCode)
                {
                    string countriesJson = await response.Content.ReadAsStringAsync();

                    JsonSerializerOptions serializerOptions = new()
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    List<Country> countries = JsonSerializer.Deserialize<List<Country>>(countriesJson, serializerOptions);

                    if (!string.IsNullOrWhiteSpace(country))
                        countries = CountriesHelper.FilterByName(countries, country);
                    if (maxPopulation.HasValue)
                        countries = CountriesHelper.FilterByPopulation(countries, maxPopulation.Value);
                    if (limit.HasValue)
                        countries = CountriesHelper.GetByLimit(countries, limit.Value);
                    if (!string.IsNullOrWhiteSpace(sortOrder))
                        countries = CountriesHelper.SortByName(countries, sortOrder);

                    return Ok(countries);
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
    }
}
