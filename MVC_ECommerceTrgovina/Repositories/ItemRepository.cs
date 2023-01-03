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
            var item= _context.Items.Where(s => s.Id == id).Select(s => new Items
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Quantity = s.Quantity,
                Price = s.Price,
                ImageName = s.ImageName,
                CategoryId = s.CategoryId,
                CategoryName = s.Category.Title



            }).FirstOrDefault();
            return item;
        }
       

    }
}
