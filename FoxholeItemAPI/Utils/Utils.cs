using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FoxholeItemAPI.Utils
{
    public static class ApiUtils
    {
        public static Category ToCategory(this string value) {
            string valueFirstUpper = Regex.Replace(value, @"\b\w", m => m.Value.ToUpper());
            Category category;

            if (!Enum.TryParse(valueFirstUpper, out category))
            {
                var categoryValues = EnumTypesToString<Category>().Values;
                var firstSimilar = categoryValues.FirstOrDefault(v =>
                {
                    int similarityScore = FuzzySharp.Fuzz.Ratio(v, value);
                    bool isSimilar = similarityScore > 60;

                    return isSimilar;
                });

                if (firstSimilar != null && Enum.TryParse(firstSimilar, out category))
                    return category;
                else
                    return Category.Unknown;
            }
            else
                return category;
        }

        public static ShippingType ToShippingType(this Category value) =>
            value switch
            {
                Category.HeavyAmmunition => ShippingType.Pallet,
                Category.Vehicles => ShippingType.CrateOrPackage,
                Category.Shippables => ShippingType.CrateOrPackage,
                Category.Resources => ShippingType.ResourceContainer,
                Category.Liquids => ShippingType.LiquidContainer,
                Category.LargeItems => ShippingType.Pallet,
                _ => ShippingType.ShippingContainer
            };

        public static Dictionary<int, string> EnumTypesToString<E>() where E : Enum
        {
            var enumTypes = Enum.GetValues(typeof(E)).Cast<int>().ToArray();
            var enumValues = Enum.GetValues(typeof(E)).Cast<E>().ToList();

            return enumValues.ToDictionary(e => enumTypes[enumValues.IndexOf(e)], e => e.ToString());
        }
    }
}
