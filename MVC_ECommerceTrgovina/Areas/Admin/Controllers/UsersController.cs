using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_ECommerceTrgovina.Data;
using MVC_ECommerceTrgovina.Models;
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
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var userRole = _context.UserRoles.FirstOrDefault(p => p.UserId == userId);
            //var roleName = _context.Roles.FirstOrDefault(s => s.Id == userRole.RoleId);
       
            var users = _context.Users.Select(
                s => new Users
                {
                    Id = s.Id,
                    Address=s.Address,
                    City= s.ZIPCode==null?"":s.ZIPCode + " "+s.City,
                    Country =s.Country,
                    Email=s.Email,
                    Rola= _context.Roles.FirstOrDefault(x => x.Id == _context.UserRoles.FirstOrDefault(p => p.UserId == s.Id).RoleId).Name,
                    Name= $"{s.FirstName} {s.LastName}",
                    LockoutEnd=s.LockoutEnd,
                }
            ).OrderBy(s=>s.LockoutEnd);
            return View(users);
        }

        // GET: UsersController/Details/5
        public ActionResult Details(string id)
        {
            if (id == "")
            {
                return RedirectToAction("Index", new { message = "Korisnik ne postoji." });
            }
            var checkUser = _context.Users.Where(s => s.Id == id);
            if (checkUser.Count() == 0)
            {
                return RedirectToAction("Index", new { message = "Korisnik ne postoji." });
            }

            var user = checkUser.Select(
                s => new Users
                {
                    Id = s.Id,
                    Address = s.Address=="" ?"Nije navedena adresa":s.Address,
                    City = s.ZIPCode == "" ? "" : s.ZIPCode + " " + s.City == "" ? "Nije naveden grad" : s.City,
                    Country = s.Country == null ? "Nije naveden grad" : s.Country,
                    Email = s.Email,
                    Rola = _context.Roles.FirstOrDefault(x => x.Id == _context.UserRoles.FirstOrDefault(p => p.UserId == id).RoleId).Name,
                    Name = $"{s.FirstName} {s.LastName}",
                    ImageName = s.ImageName,
                }).FirstOrDefault();
            return View(user);
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
        public ActionResult Create(Users korisnik, IFormFile Image)
        {
            try
            {
                var checkKorsinickoIme=_context.Users.FirstOrDefault(s=>s.Email==korisnik.Email);
                if (checkKorsinickoIme != null)
                {
                    return RedirectToAction("Create", new { msg = "Ovaj e-mail se već koristi." });
                }

                korisnik.ImageName = "";
                if (Image != null)
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
                    
                    korisnik.ImageName = image_name;
                }
                Guid guid = Guid.NewGuid();

                korisnik.Id = guid.ToString();
                var hasher = new PasswordHasher<ApplicationUser>();

                korisnik.PasswordHash = hasher.HashPassword(null, korisnik.PasswordHash);
                korisnik.Email = korisnik.Email;
                korisnik.NormalizedEmail = korisnik.Email;
                korisnik.NormalizedUserName = korisnik.Email.ToUpper();
                korisnik.UserName = korisnik.Email;
                korisnik.LockoutEnd = DateTime.Now;
                               
                _context.Add(korisnik);
                _context.SaveChanges(); 

                //Ograničavam na dvije role, admina više nije moguće dodati svi ostali su korisnici.

               var UserRoleId= _context.Roles.FirstOrDefault(s=>s.Name=="Korisnik");
               
               
                IdentityUserRole<string> identityUserRole= new IdentityUserRole<string>();
                identityUserRole.UserId = korisnik.Id;
                identityUserRole.RoleId = UserRoleId.Id;
                _context.UserRoles.Add(identityUserRole);
                _context.SaveChanges();


               
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View();
            }
        }
       


        // GET: UsersController/Edit/5
        public ActionResult Edit(string id, string? message)
        {
            if (id == "")
            {
                return RedirectToAction("Index", new { message = "Korisnik ne postoji." });
               
            }
            var checkUser = _context.Users.Where(s => s.Id == id);
            if (checkUser.Count() == 0)
            {
                return RedirectToAction("Index", new { message = "Korisnik ne postoji." });
            }
            try
            {
                var user = checkUser.Select(
               s => new Users
               {
                   Id = s.Id,
                   Address = s.Address == "" ? "Nije navedena adresa" : s.Address,
                   City = s.City == "" ? "" : s.ZIPCode + " " + s.City == "" ? "Nije naveden grad" : s.City,
                   Country = s.Country == null ? "Nije naveden grad" : s.Country,
                   Email = s.Email,
                   FirstName = s.FirstName,
                   LastName = s.LastName,
                   PhoneNumber = s.PhoneNumber,
                   Rola = _context.Roles.FirstOrDefault(x => x.Id == _context.UserRoles.FirstOrDefault(p => p.UserId == id).RoleId).Name,
                   Name = $"{s.FirstName} {s.LastName}",
                   ImageName = s.ImageName,
               }).FirstOrDefault();

                ViewBag.Message = message;

                return View(user);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Edit", new { id = id, message = ex.Message });
            }


            return View();
        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, Users model)
        {
            try
            {
                if (id == null || id == "")
                {
                    return RedirectToAction("Edit", new { product_id = id, message = "Nije odabran korisnik, došlo je do pogreške." });
                }
                var user = _context.Users.FirstOrDefault(s => s.Id == id);

                _context.Users.Update(model);
                _context.SaveChanges();


                return RedirectToAction("Edit", new { id = id, message = "Podaci korisnika su uspješno ažurirani!" });
            }
            catch(Exception ex)
            {
                return RedirectToAction("Edit", new { product_id = id, message = ex.Message });
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
