namespace Validation_Project
{
    /// <summary>
    /// Represents a basic information about country.
    /// </summary>
    public class Country
    {
        /// <summary>
        /// Name of the country.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Common name of the country.
        /// </summary>
        /// <example>
        /// Name: United States; Common Name - USA.
        /// </example>
        public string CommonName { get; set; }
    }
}
