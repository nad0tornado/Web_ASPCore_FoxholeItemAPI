using FoxholeItemAPI.Utils;

namespace FoxholeItemAPI.Interfaces
{
    public interface IFoxholeItemAPIService<Item> where Item : IItem
    {
        public Task<IEnumerable<Item>> GetItems();
        public Task<IEnumerable<Item>> GetItemsInCategory(Category category);
    }
}
