using System.Text.Json;
using System.Text.Json.Serialization;

namespace Validation_Project
{
    /// <summary>
    /// Represents a basic information about country.
    /// </summary>
    public class Country
    {
        #region Supportive classes
        /// <summary>
        /// Helps to convert country's "name/common" property into string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class NameCommonPropertyConverter<T> : JsonConverter<T>
        {
            /// <inheritdoc/>
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                using (JsonDocument doc = JsonDocument.ParseValue(ref reader))
                {
                    JsonElement nestedProperty = doc.RootElement.GetProperty("common");
                    return JsonSerializer.Deserialize<T>(nestedProperty.GetRawText(), options);
                }
            }

            /// <inheritdoc/>
            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                JsonSerializer.Serialize(writer, value, options);
            }
        }
        #endregion

        /// <summary>
        /// Name of the country.
        /// </summary>
        [JsonConverter(typeof(NameCommonPropertyConverter<string>))]
        public string Name { get; set; }

        /// <summary>
        /// Population of the country.
        /// </summary>
        public double Population { get; set; }
    }
}
