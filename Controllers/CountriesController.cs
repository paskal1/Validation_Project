using Microsoft.AspNetCore.Mvc;

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
    }
}
