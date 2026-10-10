using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

public class CustomDateTimeConverter : JsonConverter<DateTime>
{
    // Define the format for the 'start_date' string
    private static readonly string[] Formats =
    {
        "ddd, dd MMM yyyy HH:mm:ss GMT",  // Example: Tue, 28 Jan 2025 06:47:38 GMT
        "yyyy-MM-ddTHH:mm:ssZ",           // Optional: You can add any other formats as needed
        "yyyy-MM-dd HH:mm:ss"
    };

    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string dateString = reader.GetString();

        if (DateTime.TryParseExact(dateString, Formats, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out DateTime result))
        {
            return result;
        }

        throw new JsonException($"Unable to convert '{dateString}' to DateTime.");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-ddTHH:mm:ssZ"));
    }
}
