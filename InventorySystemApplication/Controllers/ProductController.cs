using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventorySystemApplication.Data;
using InventorySystemApplication.Models;
using InventorySystemApplication.VMs;


namespace InventorySystemApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            List<ProductVM> objProduct = _context.ProductsTable.Select(n => new ProductVM{
                Id = n.Id,
                Product_Name= n.Product_Name,
                Manufacturing_Date = n.Manufacturing_Date,
                Expiry_Date = n.Expiry_Date,
                Category_Id = n.Category_Id,
                Categories = _context.CategoriesTable.FirstOrDefault(c => c.Id == n.Category_Id)
            }

            ).ToList();
              return View(objProduct);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ProductsTable == null)
            {
                return NotFound();
            }

            var product = await _context.ProductsTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            var cat = _context.CategoriesTable.ToList();
            ViewBag.Categories= cat;
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Product_Name,Manufacturing_Date,Expiry_Date,Category_Id")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            

            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ProductsTable == null)
            {
                return NotFound();
            }

            var product = await _context.ProductsTable.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var cat = _context.CategoriesTable.ToList();
            ViewBag.Categories = cat;
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Product_Name,Manufacturing_Date,Expiry_Date,Category_Id")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ProductsTable == null)
            {
                return NotFound();
            }

            var product = await _context.ProductsTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ProductsTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ProductsTable'  is null.");
            }
            var product = await _context.ProductsTable.FindAsync(id);
            if (product != null)
            {
                _context.ProductsTable.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.ProductsTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
