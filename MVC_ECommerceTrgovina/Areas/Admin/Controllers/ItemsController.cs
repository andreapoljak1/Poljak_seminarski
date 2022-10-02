using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_ECommerceTrgovina.Data;
using MVC_ECommerceTrgovina.Models;

namespace MVC_ECommerceTrgovina.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Items
        public IActionResult Index(int? categoryId)
        {
            List<Items> items=new List<Items>();
            if (categoryId == null)
            {
                items = _context.Items.ToList();
               
            }
            else
            {
                items = _context.Items.Where(s=>s.CategoryId==categoryId).ToList();
            }

            ViewBag.Categories = _context.Category.ToList();

            return View(items);
        }

       
        // GET: Admin/Items/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var items = await _context.Items
                .Include(i => i.Category)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // GET: Admin/Items/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Title");
          
            return View();
        }

        // POST: Admin/Items/Create
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Items items, IFormFile Image)
        {
            try
            {
                if (Image != null)
                {
                    var image_name = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "Item-" + Image.FileName.ToLower();

                    var save_image_path = Path.Combine(
                                                Directory.GetCurrentDirectory(),
                                                "wwwroot/images",
                                                image_name
                                          );

                    using (var stream = new FileStream(save_image_path, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }

                    items.ImageName = image_name;
                }

                _context.Add(items);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View();
            }
                
          
            
            
        }

        // GET: Admin/Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var items = await _context.Items.FindAsync(id);
            if (items == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Title", items.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", items.UserId);
            return View(items);
        }

        // POST: Admin/Items/Edit/5
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Items items, IFormFile Image)
        {
            if (id != items.Id)
            {
                return NotFound();
            }

                try
                {
                if (Image != null)
                {

                    string FileName = items.ImageName;
                    string PathDelete = "wwwroot/images\\" + FileName;

                    FileInfo file = new FileInfo(PathDelete);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    var image_name = DateTime.Now.ToString("yyyy-MM-dd-hh-mm-ss") + "Item-" + Image.FileName.ToLower();

                    var save_image_path = Path.Combine(
                                                Directory.GetCurrentDirectory(),
                                                "wwwroot/images",
                                                image_name
                                          );

                    using (var stream = new FileStream(save_image_path, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }

                    items.ImageName = image_name;
                }
                _context.Update(items);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemsExists(items.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
          
          
            return View(items);
        }

        // GET: Admin/Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Items == null)
            {
                return NotFound();
            }

            var items = await _context.Items
                .Include(i => i.Category)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (items == null)
            {
                return NotFound();
            }

            return View(items);
        }

        // POST: Admin/Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Items == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Items'  is null.");
            }
            var items = await _context.Items.FindAsync(id);
            if (items != null)
            {
                _context.Items.Remove(items);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemsExists(int id)
        {
          return (_context.Items?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
