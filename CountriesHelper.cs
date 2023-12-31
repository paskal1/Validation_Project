﻿using System.Globalization;

namespace Validation_Project
{
    /// <summary>
    /// A set of basic functions for the <see cref="Country"/>.
    /// </summary>
    public static class CountriesHelper
    {
        /// <summary>
        /// Returns a list of countries filtered by name.
        /// </summary>
        /// <param name="countries">Target countries list.</param>
        /// <param name="searchString">Country name criteria.</param>
        /// <returns>A list of countries filtered by name.</returns>
        public static List<Country> FilterByName(List<Country> countries, string searchString)
        {
            ValidateList(countries);
            if (string.IsNullOrWhiteSpace(searchString))
                throw new ArgumentNullException(nameof(searchString));

            string searchLower = searchString.ToLower(CultureInfo.InvariantCulture);
            return countries.Where(country => country.Name.ToLower(CultureInfo.InvariantCulture).Contains(searchLower)).ToList();
        }

        /// <summary>
        /// Returns a list of countries filtered by population.
        /// </summary>
        /// <param name="countries">Target countries list.</param>
        /// <param name="maxPopulation">Country population criteria.</param>
        /// <returns>A list of countries filtered by population.</returns>
        /// <remarks>A value of <paramref name="maxPopulation"/> is specified in millions (e.g., by providing value `10`, method will return countries with a population less than 10m).</remarks>
        public static List<Country> FilterByPopulation(List<Country> countries, int maxPopulation)
        {
            ValidateList(countries);
            List<Country> filteredCountries = countries.Where(country => country.Population < maxPopulation * 1000000).ToList();
            return filteredCountries;
        }

        /// <summary>
        /// Returns a list of countries sorted by name according to <paramref name="sortOrder"/>.
        /// </summary>
        /// <param name="countries">Target countries list.</param>
        /// <param name="sortOrder">Order of sorting. Must be <see langword="ascend"/> or <see langword="descend"/>.</param>
        /// <returns>A list of countries sorted by <paramref name="sortOrder"/>.</returns>
        public static List<Country> SortByName(List<Country> countries, string sortOrder)
        {
            ValidateList(countries);
            if (sortOrder.Equals("ascend", StringComparison.OrdinalIgnoreCase))
                return countries.OrderBy(country => country.Name).ToList();
            if (sortOrder.Equals("descend", StringComparison.OrdinalIgnoreCase))
                return countries.OrderByDescending(country => country.Name).ToList();

            throw new ArgumentException(Constants.Exceptions.InvalidSortOrder);
        }

        /// <summary>
        /// Returns a list of countries limited by <paramref name="limit"/>.
        /// </summary>
        /// <param name="countries">Target countries list.</param>
        /// <param name="limit">Returned countries are limited by this parameter.</param>
        /// <returns>A list of countries limited by <paramref name="limit"/>.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static List<Country> GetByLimit(List<Country> countries, int limit)
        {
            ValidateList(countries);
            if (limit <= 0)
                throw new ArgumentException(Constants.Exceptions.InvalidLimitNumber);

            return countries.Take(limit).ToList();
        }

        private static void ValidateList(List<Country> countries)
        {
            if (countries is null)
                throw new ArgumentNullException(nameof(countries));
        }
    }
}
