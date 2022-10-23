using MVC_ECommerceTrgovina.Data;
using MVC_ECommerceTrgovina.Models;
using MVC_ECommerceTrgovina.Interfaces;

namespace MVC_ECommerceTrgovina.Repositories
{
    public class ItemRepository: IItemsRepository
    {
        private readonly ApplicationDbContext _context;

        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Items> GetAll()
        {
            return _context.Items.ToList();
        }
        public Items GetItemById(int id)
        {
            return _context.Items.FirstOrDefault(s => s.Id == id);
        }
       

    }
}
