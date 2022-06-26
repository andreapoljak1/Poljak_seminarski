using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC_ECommerceTrgovina.Areas.Admin.Models;
using MVC_ECommerceTrgovina.Data;
using System.Security.Claims;

namespace MVC_ECommerceTrgovina.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;

        }
        // GET: UsersController
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRole = _context.UserRoles.FirstOrDefault(p => p.UserId == userId);
            var roleName = _context.Roles.FirstOrDefault(s => s.Id == userRole.RoleId);
            ViewBag.UserRole = roleName.Name;
            var users = _context.Users.Select(
                s => new Users
                {
                    Id = s.Id,
                    Rola= _context.Roles.FirstOrDefault(x => x.Id == _context.UserRoles.FirstOrDefault(p => p.UserId == s.Id).UserId).Name,
                    Name = s.FirstName+" "+s.LastName,
                    Address=s.Address,
                    City= s.ZIPCode==null?"":s.ZIPCode + " "+s.City,
                    Country =s.Country,
                    Email=s.Email
                }
            ).ToList();
            return View(users);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UsersController/Create
        public ActionResult Create()
        {
            ViewBag.Role = _context.Roles.Where(s=>s.Name != "Admin").ToList();
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
