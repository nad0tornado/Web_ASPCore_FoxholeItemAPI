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
            string categoryStr = jsonDict["itemCategory"].GetString() ?? string.Empty;
            Category category = categoryStr.ToCategory();

            bool hasSubCategory = jsonDict.ContainsKey("itemSubCategory");
            if (!hasSubCategory)
                return new Item(iconName, displayName, category, category.ToShippingType());

            string? subCategoryStr = jsonDict["itemSubCategory"].GetString();
            Category subCategory = subCategoryStr?.ToCategory() ?? Category.Unknown;

            return new Item(iconName, displayName, category, subCategory.ToShippingType(), subCategory);
        }

        public override void Write(Utf8JsonWriter writer, Item value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("imgName", value.IconName);
            writer.WriteString("itemName", value.DisplayName);
            writer.WriteString("itemCategory", value.Category.ToString());
            writer.WriteString("itemSubCategory", value.SubCategory.ToString());
            writer.WriteEndObject();
        }
    }
}
