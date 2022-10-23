using MVC_ECommerceTrgovina.Models;

namespace MVC_ECommerceTrgovina.Interfaces
{
    public interface IItemsRepository
    {
        IEnumerable<Items> GetAll();
        Items GetItemById(int id);
     
    }
}
