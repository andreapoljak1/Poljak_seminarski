using Microsoft.AspNetCore.Mvc;
using MVC_ECommerceTrgovina.Data;
using MVC_ECommerceTrgovina.Models;
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
                items = _context.Items.ToList();

            }
            else
            {
                items = _context.Items.Where(s => s.CategoryId == categoryId).ToList();
            }

            ViewBag.Categories = _context.Category.ToList();

            return View(items);
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