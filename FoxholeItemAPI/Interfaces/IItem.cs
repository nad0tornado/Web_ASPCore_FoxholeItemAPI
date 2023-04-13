using FoxholeItemAPI.Utils;
using System.Text.Json.Serialization;

namespace FoxholeItemAPI.Interfaces
{
    public interface IItem
    {
        public string IconName { get; }

        public string DisplayName { get; }

        public Category Category { get; }

        public Category SubCategory { get; set; }

        public ShippingType ShippingType { get; }

        public Faction Faction { get; }
    }
}
