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
                   City = s.City == "" ? ""  : s.City,
                   ZIPCode= s.ZIPCode == "" ? "" : s.ZIPCode,
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


        }

        // POST: UsersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string Id, Users model, IFormFile Image)
        {
            try
            {
                if (Id == null || Id == "")
                {
                    return RedirectToAction("Edit", new { product_id = Id, message = "Nije odabran korisnik, došlo je do pogreške." });
                }
                var checkKorsinickoIme = _context.Users.FirstOrDefault(s => s.Email == model.Email);
                if (checkKorsinickoIme != null)
                {
                    return RedirectToAction("Create", new { message = "Ovaj e-mail se već koristi." });
                }
                var user = _context.Users.FirstOrDefault(s => s.Id == Id);
                if(user != null)
                {
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

                        user.ImageName = image_name;
                    }
                    user.Email = model.Email;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.NormalizedEmail = model.Email;
                    user.NormalizedUserName = model.Email.ToUpper();
                    user.UserName = model.Email;
                    user.Address = model.Address;
                    user.ZIPCode = model.ZIPCode;
                    user.PhoneNumber = model.PhoneNumber;
                    user.City = model.City;

                    _context.SaveChanges();


                    return RedirectToAction("Edit", new { id = Id, message = "Podaci korisnika su uspješno ažurirani!" });
                }
                else
                {
                    return RedirectToAction("Edit", new { id = Id, message = "Korisnik nije pronađen u bazi, došlo je do pogreške." });
                }
                
            }
            catch(Exception ex)
            {
                return RedirectToAction("Edit", new { id = Id, message = ex.Message });
            }
        }

        // GET: UsersController/Delete/5
        public ActionResult Delete(string id, string? error_message)
        {
            if (id == "")
            {
                return RedirectToAction("Index");
            }
            var korisnik = _context.Users.SingleOrDefault(p => p.Id == id);

           if (korisnik == null)
            {
                return RedirectToAction("Index", new { msg = "Korisnik ne postoji, dogodila se pogreška." });
            }

            ViewBag.KorisnikErrorMessage = error_message;

            return View(korisnik);
        }

        // POST: UsersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, IFormCollection collection)
        {
            if (id == "")
            {
                return RedirectToAction("Index");
            }

            try
            {
                var find_User = _context.Users.FirstOrDefault(s => s.Id == id);

                if (find_User == null)
                {
                    return View("Delete", new { product_id = id, msg = "Korisnik ne postoji." });
                }

                var ifInItems = _context.Items.FirstOrDefault(s => s.UserId == id);
                if (ifInItems != null)
                {
                    return View("Delete", new { product_id = id, msg = "Korisnik je već dodavao proizvode i nije ga moguće brisati." });
                }
              
                _context.Users.Remove(find_User);
                _context.SaveChanges();

              

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction("Delete", new { error_message = ex.InnerException.Message });
            }
        }
    }
}
