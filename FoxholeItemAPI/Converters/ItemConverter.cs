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

            var factions = jsonDict.ContainsKey("faction") ? jsonDict["faction"].EnumerateArray().Select(e => e.GetString()) : new string[0];
            Faction faction = Faction.Neutral;

            if (factions.Contains("warden") && !factions.Contains("colonial"))
                faction = Faction.Warden;
            else if (factions.Contains("colonial") && !factions.Contains("warden"))
                faction = Faction.Colonial;
            else
                faction = Faction.Neutral;


            string categoryStr = jsonDict["itemCategory"].GetString() ?? string.Empty;
            Category category = categoryStr.ToCategory();

            bool hasSubCategory = jsonDict.ContainsKey("itemSubCategory");
            if (!hasSubCategory)
                return new Item(iconName, displayName, category, category.ToShippingType(), faction);

            string? subCategoryStr = jsonDict["itemSubCategory"].GetString();
            Category subCategory = subCategoryStr?.ToCategory() ?? Category.Unknown;

            return new Item(iconName, displayName, category, subCategory.ToShippingType(), faction, subCategory);
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
