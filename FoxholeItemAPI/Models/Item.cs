using FoxholeItemAPI.Interfaces;
using FoxholeItemAPI.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoxholeItemAPI.Models
{
    public record Item : IItem
    {
        public string IconName { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public Category Category { get; set; }

        public Category SubCategory { get; set; }

        public ShippingType ShippingType { get; set; }

        public Item() { }
        public Item(string iconName, string displayName, Category category, ShippingType shippingType, Category subCategory = Category.Unknown)
        {
            IconName = iconName;
            DisplayName = displayName;
            Category = category;
            ShippingType = shippingType;
            SubCategory = subCategory;
        }

        public override string ToString()
        {
            var options = new JsonSerializerOptions
            {
                Converters =
                {
                    new JsonStringEnumConverter()
                }
            };

            return JsonSerializer.Serialize(this, options);
        }
    }
}
