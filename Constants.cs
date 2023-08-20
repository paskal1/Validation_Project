namespace Validation_Project
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
            public const string All = $"{BaseAddress}/all?fields=name,population";
        }

        /// <summary>
        /// Represents a set of exception messages.
        /// </summary>
        public static class Exceptions
        {
            public const string InvalidSortOrder = "Invalid sort order. Please provide 'ascend' or 'descend'!";
            public const string InvalidLimitNumber = "Limit must be a positive integer.";
        }
    }
}
