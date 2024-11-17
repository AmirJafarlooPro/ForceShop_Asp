using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForceShop.Data.Contex;
using ForceShop.Domian.Models.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace ForceShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ForceShopContex _context;

        public ProductsController(ForceShopContex context)
        {
            _context = context;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var forceShopContex = _context.Products.Include(p => p.ProductGroup);
            return View(await forceShopContex.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductGroup)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["GroupID"] = new SelectList(_context.ProductGroups, "ID", "GroupTitle");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, IFormFile[] imgUp)
        {
            if (ModelState.IsValid)
            {
                product.CreateDate = DateTime.Now;
                _context.Add(product);
                _context.SaveChanges();

                if (imgUp != null && imgUp.Any())
                {
                    foreach (var img in imgUp)
                    {
                        string imageName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                        string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", imageName);

                        using (var stream = new FileStream(savePath, FileMode.Create))
                        {
                            img.CopyTo(stream);
                        }

                        _context.ProductImages.Add(new ProductImage()
                        {
                            CreateDate = DateTime.Now,
                            ImageName = imageName,
                            IsDelete = false,
                            ProductID = product.ID
                        });
                    }

                    _context.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupID"] = new SelectList(_context.ProductGroups, "ID", "GroupTitle", product.GroupID);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["GroupID"] = new SelectList(_context.ProductGroups, "ID", "GroupTitle", product.GroupID);
            ViewData["Images"] = _context.ProductImages.Where(p => p.ProductID == id.Value).ToList();
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile[] imgUp)
        {
            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    if (imgUp != null && imgUp.Any())
                    {
                        foreach (var img in imgUp)
                        {
                            string imageName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", imageName);

                            using (var stream = new FileStream(savePath, FileMode.Create))
                            {
                                img.CopyTo(stream);
                            }

                            _context.ProductImages.Add(new ProductImage()
                            {
                                CreateDate = DateTime.Now,
                                ImageName = imageName,
                                IsDelete = false,
                                ProductID = product.ID
                            });
                        }
                    }
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupID"] = new SelectList(_context.ProductGroups, "ID", "GroupTitle", product.GroupID);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductGroup)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                product.IsDelete = true;
                _context.Products.Update(product);
            }

            var images = _context.ProductImages.Where(p=>p.ProductID == id).ToList();

            foreach (var image in images )
            {
                string DeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", image.ImageName);

                if (System.IO.File.Exists(DeletePath))
                {
                    System.IO.File.Delete(DeletePath);
                }
                _context.ProductImages.Remove(image);
            }

            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ID == id);
        }

        public void DeleteImage(int id)
        {
            var image = _context.ProductImages.Find(id);
            if (image != null)
            {
                _context.ProductImages.Remove(image);
                string DeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Images", image.ImageName);

                if (System.IO.File.Exists(DeletePath))
                {
                    System.IO.File.Delete(DeletePath);
                }
                _context.SaveChanges();
            }
        }

    }
}

