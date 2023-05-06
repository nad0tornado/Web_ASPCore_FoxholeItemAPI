using FoxholeItemAPI.Utils;

namespace FoxholeItemAPI.Interfaces
{
    public interface IFoxholeItemAPIService<Item> where Item : IItem
    {
        public Task<List<Item>> GetItems();
        public Task<List<Item>> GetItemsInCategory(Category category);
    }
}
