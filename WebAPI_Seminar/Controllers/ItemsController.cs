using Microsoft.AspNetCore.Mvc;
using MVC_ECommerceTrgovina.Models;
using MVC_ECommerceTrgovina.Interfaces;

namespace WebAPI_Seminar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly IItemsRepository _itemsRepository;

        //konstruktor
        public ItemsController(IItemsRepository itemsRepository)
        {
            _itemsRepository = itemsRepository;
        }
        //GET: api/Items
        [HttpGet]
        public ActionResult<IEnumerable<Items>> GetItems()
        {
            
            try
            {
                return Ok(_itemsRepository.GetAll());
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
        //GET: api/items/5
        [HttpGet("{id}")]
        public IActionResult FindItem(int id)
        {
            try
            {
                var item = _itemsRepository.GetItemById(id);
                if (item == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Rezultat nije pronađen");
                }
                return Ok(item);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Nije moguće prikazati rezultate, dogodila se greška!");
            }

        }
    }
}
