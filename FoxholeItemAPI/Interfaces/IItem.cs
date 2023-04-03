using FoxholeItemAPI.Utils;

namespace FoxholeItemAPI.Interfaces
{
    public interface IItem
    {
        public string IconName { get; }

        public string DisplayName { get; }

        public Category Category { get; }

        public ShippingType ShippingType { get; }
    }
}
