﻿namespace Validation_Project.Controllers
{
    /// <summary>
    /// Global constants.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Represents a set of endpoints related to the <see href="https://restcountries.com">RestCountries</see>.
        /// </summary>
        public static class RestCountries
        {
            public const string HttpClientName = nameof(RestCountries);
            public const string BaseAddress = "https://restcountries.com/v3.1";
            public const string All = $"{BaseAddress}/all";
        }
    }
}
