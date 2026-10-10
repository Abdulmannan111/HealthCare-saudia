using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace API.Middlewares
{
    public class LicenseNumberConverter : JsonConverter<object>
    {
        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            if (value is JsonElement jsonElement)
            {
                jsonElement.WriteTo(writer);  // Write JsonElement as raw JSON
            }
            else
            {
                JsonSerializer.Serialize(writer, value, options);  // Serialize other types normally
            }
        }
    }
}
