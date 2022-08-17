using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<ApplicationUser> _userManager;
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
                s => new ApplicationUser
                {
                    Id = s.Id,
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
        public ActionResult Create(string? msg)
        {
            ViewBag.msg = msg;
            ViewBag.Role = _context.Roles.Where(s=>s.Name != "Admin").ToList();
            return View();
        }

        // POST: UsersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicationUser korisnik, IFormFile Image)
        {
            try
            {
                var checkKorsinickoIme=_context.Users.FirstOrDefault(s=>s.Email==korisnik.Email);
                if (checkKorsinickoIme != null)
                {
                    return RedirectToAction("Create", new { msg = "Ovaj e-mail se već koristi." });
                }


                if(Image != null)
                {
                    var image_name = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "-" + Image.FileName.ToLower();

                    var save_image_path = Path.Combine(
                                                Directory.GetCurrentDirectory(),
                                                "wwwroot/images",
                                                image_name
                                          );

                    using (var stream = new FileStream(save_image_path, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }

                    korisnik.Image = image_name;
                }
                Guid guid = Guid.NewGuid();

                korisnik.Id = guid.ToString();
                var hasher = new PasswordHasher<ApplicationUser>();

                korisnik.PasswordHash = hasher.HashPassword(null, korisnik.PasswordHash);

                korisnik.NormalizedEmail = korisnik.Email;
                korisnik.NormalizedUserName = korisnik.Email.ToUpper();
                korisnik.UserName = korisnik.Email;
                               
                _context.Add(korisnik);
                _context.SaveChanges(); 

                //Ograničavam na dvije role, admina više nije moguće dodati svi ostali su korisnici.

               var UserRoleId= _context.Roles.FirstOrDefault(s=>s.Name=="Korisnik");
               
                CreateRolesandUsers(korisnik, UserRoleId.Name);

                //Nije razvijano dalje jer se ne znam izvući iz ovog spremanja... :(
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public async Task CreateRolesandUsers(ApplicationUser user,string roleName)
        {

            IdentityResult chkUser = await _userManager.CreateAsync(user, user.PasswordHash);

            //pokušaj dodavanja u tablicu AspNetUserRoles   
            if (chkUser.Succeeded)
            {
                var result = await _userManager.AddToRoleAsync(user, roleName);
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
