using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using FoxholeItemAPI.Models;
using FoxholeItemAPI.Utils;

namespace FoxholeItemAPI.Converters
{
    public class ItemConverter : JsonConverter<Item>
    {
        public override Item Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Read the JSON object into a dictionary
            var jsonDict = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(ref reader, options);

            if (jsonDict == null)
                throw new NullReferenceException("Failed to map Item data. Check input and try again");

            if (!jsonDict.ContainsKey("itemName"))
                throw new NullReferenceException("\"itemName\" is required");
            if (!jsonDict.ContainsKey("imgName"))
                throw new NullReferenceException("\"imgName\" is required");
            if (!jsonDict.ContainsKey("itemCategory"))
                throw new NullReferenceException("\"itemCategory\" is required");

            string iconName = jsonDict["imgName"].GetString() ?? string.Empty;
            string displayName = jsonDict["itemName"].GetString() ?? string.Empty;
            string? categoryStr = jsonDict["itemCategory"].GetString();
            Category category = categoryStr.ToCategory();

            return new Item (iconName, displayName, category, category.ToShippingType());
        }

        public override void Write(Utf8JsonWriter writer, Item value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("imgName", value.IconName);
            writer.WriteString("itemName", value.DisplayName);
            writer.WriteString("itemCategory", value.Category.ToString());
            writer.WriteEndObject();
        }
    }
}
