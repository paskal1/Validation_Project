using Xunit;

namespace Validation_Project.Tests
{
    /// <summary>
    /// Unit tests for the class <see cref="CountriesHelper"/>.
    /// </summary>
    public class CountriesHelperTests
    {
        private static List<Country> _Countries = new()
        {
            new Country { Name = "Canada", Population = 38000000 },
            new Country { Name = "United States", Population = 331000000 },
            new Country { Name = "Mexico", Population = 126000000 },
            new Country { Name = "Australia", Population = 256900000 }
        };

        #region FilterByName
        [Fact]
        public void FilterByName_WhenDataIsFound_ReturnsMatchingCountries()
        {
            // Arrange
            List<Country> countries = _Countries;

            // Act
            List<Country> filteredCountries = CountriesHelper.FilterByName(countries, "united");

            // Assert
            Assert.Equal(1, filteredCountries.Count);
            Assert.Equal("United States", filteredCountries[0].Name);
        }

        [Fact]
        public void FilterByName_WhenDataIsNotFound_ReturnsEmptyList()
        {
            // Arrange
            List<Country> countries = new List<Country>
            {
                new Country() { Name = "Canada" }
            };

            // Act
            List<Country> filteredCountries = CountriesHelper.FilterByName(countries, "australia");

            // Assert
            Assert.Empty(filteredCountries);
        }

        [Fact]
        public void FilterByName_WhenListIsNullOrEmpty_MustThrowException()
        {
            // Arrange
            List<Country> countries = null;

            // Act
            var filterCountries1 = () => CountriesHelper.FilterByName(countries, "_");
            var filterCountries2 = () => CountriesHelper.FilterByName(new List<Country>(), "_");

            // Assert
            Assert.Throws<ArgumentNullException>(filterCountries1);
            Assert.Throws<ArgumentNullException>(filterCountries2);
        }

        [Fact]
        public void FilterByName_WhenSearchStringIsNullOrEmpty_MustThrowException()
        {
            // Arrange
            List<Country> countries = _Countries;

            // Act
            var filterCountries1 = () => CountriesHelper.FilterByName(countries, string.Empty);
            var filterCountries2 = () => CountriesHelper.FilterByName(countries, null);

            // Assert
            Assert.Throws<ArgumentNullException>(filterCountries1);
            Assert.Throws<ArgumentNullException>(filterCountries2);
        }
        #endregion

        #region FilterByPopulation
        [Fact]
        public void FilterByPopulation_WhenDataIsFound_ReturnsMatchingCountries()
        {
            // Arrange
            List<Country> countries = _Countries;

            // Act
            List<Country> filteredCountries = CountriesHelper.FilterByPopulation(countries, 200);

            // Assert
            Assert.Equal(2, filteredCountries.Count);
            Assert.Contains(filteredCountries, country => country.Name == "Canada");
            Assert.Contains(filteredCountries, country => country.Name == "Mexico");
        }

        [Fact]
        public void FilterByPopulation_WhenDataIsNotFound_ReturnsEmptyList()
        {
            // Arrange
            List<Country> countries = _Countries;

            // Act
            List<Country> filteredCountries = CountriesHelper.FilterByPopulation(countries, 20);

            // Assert
            Assert.Empty(filteredCountries);
        }

        [Fact]
        public void FilterByPopulation_WhenListIsNullOrEmpty_ThrowsException()
        {
            // Arrange
            List<Country> countries = null;

            // Act
            var filterCountries1 = () => CountriesHelper.FilterByPopulation(countries, 10);
            var filterCountries2 = () => CountriesHelper.FilterByPopulation(new List<Country>(), 10);

            // Assert
            Assert.Throws<ArgumentNullException>(filterCountries1);
            Assert.Throws<ArgumentNullException>(filterCountries2);
        }
        #endregion

        #region SortByName
        [Fact]
        public void SortByName_WhenParametersAreCorrect_SortsAscending()
        {
            // Arrange
            List<Country> countries = _Countries;

            // Act
            List<Country> sortedCountries = CountriesHelper.SortByName(countries, "ascend");

            // Assert
            Assert.Equal("Australia", sortedCountries[0].Name);
            Assert.Equal("Canada", sortedCountries[1].Name);
            Assert.Equal("Mexico", sortedCountries[2].Name);
            Assert.Equal("United States", sortedCountries[3].Name);
        }

        [Fact]
        public void SortByName_WhenParametersAreCorrect_SortsDescending()
        {
            // Arrange
            List<Country> countries = _Countries;

            // Act
            List<Country> sortedCountries = CountriesHelper.SortByName(countries, "descend");

            // Assert
            Assert.Equal("United States", sortedCountries[0].Name);
            Assert.Equal("Mexico", sortedCountries[1].Name);
            Assert.Equal("Canada", sortedCountries[2].Name);
        }

        [Fact]
        public void SortByName_WhenSortParameterIsInvalid_ThrowsException()
        {
            // Arrange
            List<Country> countries = _Countries;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => CountriesHelper.SortByName(countries, "invalid"));
        }

        [Fact]
        public void SortByName_WhenCountriesAreNullOrEmpty_ThrowsException()
        {
            // Arrange
            List<Country> countries = null;

            // Act
            var filterCountries1 = () => CountriesHelper.SortByName(countries, "descend");
            var filterCountries2 = () => CountriesHelper.SortByName(new List<Country>(), "descend");

            // Assert
            Assert.Throws<ArgumentNullException>(filterCountries1);
            Assert.Throws<ArgumentNullException>(filterCountries2);
        }
        #endregion

        #region GetByLimit
        [Fact]
        public void GetByLimit_WhenParametersAreCorrect_ReturnsLimitedCountries()
        {
            // Arrange
            List<Country> countries = _Countries;

            int limit = 2;

            // Act
            List<Country> limitedCountries = CountriesHelper.GetByLimit(countries, limit);

            // Assert
            Assert.Equal(limit, limitedCountries.Count);
            Assert.Equal("Canada", limitedCountries[0].Name);
            Assert.Equal("United States", limitedCountries[1].Name);
        }

        [Fact]
        public void GetByLimit_WhenLimitIsInvalid_ThrowsException()
        {
            // Arrange
            List<Country> countries = _Countries;

            int invalidLimit1 = 0;
            int invalidLimit2 = 0;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => CountriesHelper.GetByLimit(countries, invalidLimit1));
            Assert.Throws<ArgumentException>(() => CountriesHelper.GetByLimit(countries, invalidLimit2));
        }

        [Fact]
        public void GetByLimit_WhenCountriesAreNullOrEmpty_ThrowsException()
        {
            // Arrange
            List<Country> countries = null;

            // Act
            var filterCountries1 = () => CountriesHelper.GetByLimit(countries, 1);
            var filterCountries2 = () => CountriesHelper.GetByLimit(new List<Country>(), 1);

            // Assert
            Assert.Throws<ArgumentNullException>(filterCountries1);
            Assert.Throws<ArgumentNullException>(filterCountries2);
        }
        #endregion
    }
}