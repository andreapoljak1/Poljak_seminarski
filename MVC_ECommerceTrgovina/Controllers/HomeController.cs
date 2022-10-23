using Microsoft.AspNetCore.Mvc;
using MVC_ECommerceTrgovina.Data;
using MVC_ECommerceTrgovina.Models;
using MVC_ECommerceTrgovina.Repositories;
using Newtonsoft.Json;
using System.Diagnostics;

namespace MVC_ECommerceTrgovina.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int? categoryId)
        {
            List<Items> items = new List<Items>();
            if (categoryId == null)
            {
                //Koristim api za dohvat liste proizvoda
                items = AllItemsApi();

            }
            else
            {
                items = _context.Items.Where(s => s.CategoryId == categoryId).ToList();
            }

            ViewBag.Categories = _context.Category.ToList();

            return View(items);
        }


        public IActionResult Detalji(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
           
            string apiUrl = "https://localhost:7174/api/Items";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl + string.Format("/{0}", id)).Result;
            Items item= new Items();
            if (response.IsSuccessStatusCode)
            {
                item = JsonConvert.DeserializeObject<Items>(response.Content.ReadAsStringAsync().Result);
            }


            return View("Details", item);
        }

        private static List<Items> AllItemsApi()
        {
            List<Items> proizvodi = new List<Items>();
            string apiUrl = "https://localhost:7174/api/Items";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(apiUrl).Result;
            if (response.IsSuccessStatusCode)
            {
                proizvodi = JsonConvert.DeserializeObject<List<Items>>(response.Content.ReadAsStringAsync().Result);
            }

            return proizvodi;
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}