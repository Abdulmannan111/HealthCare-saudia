using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Application.Resolvers
{
    public static class ObjectCleaner
    {
        public static object RemoveNullAndEmptyProperties(object obj)
        {
            if (obj == null) return new { }; // Return empty object if null

            // Serialize and Deserialize to ensure we work with a dictionary
            var json = JsonSerializer.Serialize(obj);
            var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(json);

            var cleanedDict = dict
                .Where(kv => IsNotNullOrEmpty(kv.Value))
                .ToDictionary(kv => kv.Key, kv => CleanValue(kv.Value));

            return cleanedDict;
        }

        private static bool IsNotNullOrEmpty(object value)
        {
            if (value == null) return false;
            if (value is string str && string.IsNullOrWhiteSpace(str)) return false;
            if (value is IEnumerable<object> list && !list.Any()) return false;
            if (value is JsonElement jsonElement) return IsNotNullOrEmpty(JsonElementToObject(jsonElement));
            return true;
        }

        private static object CleanValue(object value)
        {
            if (value is JsonElement jsonElement)
            {
                value = JsonElementToObject(jsonElement);
            }

            if (value is Dictionary<string, object> dict)
            {
                return dict.Where(kv => IsNotNullOrEmpty(kv.Value))
                           .ToDictionary(kv => kv.Key, kv => CleanValue(kv.Value));
            }

            if (value is IEnumerable<object> list)
            {
                var cleanedList = list.Select(CleanValue).Where(IsNotNullOrEmpty).ToList();
                return cleanedList.Any() ? cleanedList : null;
            }

            return value;
        }

        private static object JsonElementToObject(JsonElement element)
        {
            switch (element.ValueKind)
            {
                case JsonValueKind.Object:
                    return element.EnumerateObject()
                                  .ToDictionary(p => p.Name, p => JsonElementToObject(p.Value));
                case JsonValueKind.Array:
                    return element.EnumerateArray()
                                  .Select(JsonElementToObject)
                                  .ToList();
                case JsonValueKind.String:
                    return element.GetString();
                case JsonValueKind.Number:
                    return element.GetDouble();
                case JsonValueKind.True:
                    return true;
                case JsonValueKind.False:
                    return false;
                case JsonValueKind.Null:
                    return null;
                default:
                    return element.ToString();
            }
        }
    }

}
