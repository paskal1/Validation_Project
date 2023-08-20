﻿using Microsoft.AspNetCore.Mvc;
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
        private static List<Country> FilterByPopulation(List<Country> countries, double maxPopulation)
        {
            List<Country> filteredCountries = countries.Where(country => country.Population < maxPopulation).ToList();
            return filteredCountries;
        }
    }
}
