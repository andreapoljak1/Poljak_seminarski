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
        private IConfiguration _configuration;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IConfiguration iconfig)
        {
            _logger = logger;
            _context = context;
            _configuration = iconfig;
        }

        public IActionResult Index(int? categoryId, int? locationID)
        {
            List<Items> items = new List<Items>();
            Random random = new Random();
            if (categoryId == null && locationID==null)
            {
                //Koristim api za dohvat liste proizvoda

                items = AllItemsApi();
                items = items.OrderBy(x => random.Next(items.Count)).Take(10).ToList();

            }
            else
            {
                if(locationID != null)
                {
                    categoryId=locationID.Value;
                }
                items = _context.Items.Where(s => s.CategoryId == categoryId).ToList();
                items = items.OrderBy(x => random.Next(items.Count)).Take(10).ToList();
            }
            var categories= _context.Category.ToList();
            ViewBag.Categories = categories;
            ViewBag.CategoriesCount = categories.Count();
            return View(items);
        }

      
        public IActionResult Detalji(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            string myapiUrl = _configuration.GetValue<string>("apiUrl");
            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(myapiUrl + string.Format("/{0}", id)).Result;
            Items item= new Items();
            if (response.IsSuccessStatusCode)
            {
                item = JsonConvert.DeserializeObject<Items>(response.Content.ReadAsStringAsync().Result);
            }


            return View("Details", item);
        }
      
        public List<Items> AllItemsApi()
        {
            List<Items> proizvodi = new List<Items>();
            string myapiUrl = _configuration.GetValue<string>("apiUrl");


            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(myapiUrl).Result;
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