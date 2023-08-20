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
        public async Task<ActionResult> Get([FromQuery] string? country = null, [FromQuery] int? maxPopulation = null, [FromQuery] string? param3 = null, [FromQuery] string? param4 = null)
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

        /// <summary>
        /// Returns a list of countries filtered by name.
        /// </summary>
        /// <param name="countries">Target countries list.</param>
        /// <param name="searchString">Country name criteria.</param>
        /// <returns>A list of countries filtered by name.</returns>
        private static List<Country> FilterByName(List<Country> countries, string searchString)
        {
            string searchLower = searchString.ToLower(CultureInfo.InvariantCulture);
            return countries
                .Where(country =>
                    country.Name.ToLower(CultureInfo.InvariantCulture).Contains(searchLower) ||
                    country.CommonName.ToLower(CultureInfo.InvariantCulture).Contains(searchLower))
                .ToList();
        }

        /// <summary>
        /// Returns a list of countries filtered by population.
        /// </summary>
        /// <param name="countries">Target countries list.</param>
        /// <param name="maxPopulation">Country population criteria.</param>
        /// <returns>A list of countries filtered by population.</returns>
        /// <remarks>A value of <paramref name="maxPopulation"/> is specified in millions (e.g., by providing value `10`, method will return countries with a population less than 10m).</remarks>
        private static List<Country> FilterByPopulation(List<Country> countries, int maxPopulation)
        {
            List<Country> filteredCountries = countries.Where(country => country.Population < maxPopulation * 1000000).ToList();
            return filteredCountries;
        }

        /// <summary>
        /// Returns a list of countries sorted by name/common according to <paramref name="sortOrder"/>.
        /// </summary>
        /// <param name="countries">Target countries list.</param>
        /// <param name="sortOrder">Order of sorting. Must be <see langword="ascend"/> or <see langword="descend"/>.</param>
        /// <returns>A list of countries sorted by <paramref name="sortOrder"/>.</returns>
        private static List<Country> SortByAscDesc(List<Country> countries, string sortOrder)
        {
            if (sortOrder.Equals("ascend", StringComparison.OrdinalIgnoreCase))
                return countries.OrderBy(country => country.CommonName).ToList();
            else if (sortOrder.Equals("descend", StringComparison.OrdinalIgnoreCase))
                return countries.OrderByDescending(country => country.CommonName).ToList();
            
            throw new ArgumentException(Constants.Exceptions.InvalidSortOrder);
        }

        /// <summary>
        /// Returns a list of countries limited by <paramref name="limit"/>.
        /// </summary>
        /// <param name="countries">Target countries list.</param>
        /// <param name="limit">Returned countries are limited by this parameter.</param>
        /// <returns>A list of countries limited by <paramref name="limit"/>.</returns>
        /// <exception cref="ArgumentException"></exception>
        private static List<Country> GetLimitedCountries(List<Country> countries, int limit)
        {
            if (limit <= 0)
                throw new ArgumentException(Constants.Exceptions.InvalidLimitNumber);

            return countries.Take(limit).ToList();
        }
    }
}
