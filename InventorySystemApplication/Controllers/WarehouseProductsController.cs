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
    public class WarehouseProductsController : Controller
    {
        private readonly IHttpContextAccessor contxt;
        private readonly ApplicationDbContext _context;

        public WarehouseProductsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            contxt = httpContextAccessor;
        }

        // GET: WarehouseProducts
        public async Task<IActionResult> Index()
        {
            //return _context.WarehouseProductTable != null ? 
            //            View(await _context.WarehouseProductTable.ToListAsync()) :
            //            Problem("Entity set 'ApplicationDbContext.WarehouseProductTable'  is null.");
            List<UserProductVM> objProductsList = _context.WarehouseProductTable.Select(n => new UserProductVM
            {
                Id = n.Id,
                Warehouse_Id = n.Warehouse_Id,
                Product_Id = n.Product_Id,
                Products = _context.ProductsTable.FirstOrDefault(p => p.Id == n.Product_Id),
                Warehouses = _context.WarehousesTable.FirstOrDefault(w => w.Id == n.Warehouse_Id)

            }).ToList();
            return View(objProductsList);
        }

        // GET: WarehouseProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WarehouseProductTable == null)
            {
                return NotFound();
            }

            var warehouseProduct = await _context.WarehouseProductTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warehouseProduct == null)
            {
                return NotFound();
            }

            return View(warehouseProduct);
        }

        // GET: WarehouseProducts/Create
        public IActionResult Create()
        {
            var prod = _context.ProductsTable.ToList();
            ViewBag.Products = prod;
            var war = _context.WarehousesTable.ToList();
            ViewBag.Warehouses = war;
            return View();
        }

        // POST: WarehouseProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Product_Id,Warehouse_Id")] WarehouseProduct warehouseProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(warehouseProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(warehouseProduct);
        }

        // GET: WarehouseProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WarehouseProductTable == null)
            {
                return NotFound();
            }

            var warehouseProduct = await _context.WarehouseProductTable.FindAsync(id);
            if (warehouseProduct == null)
            {
                return NotFound();
            }
            return View(warehouseProduct);
        }

        // POST: WarehouseProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Product_Id,Warehouse_Id")] WarehouseProduct warehouseProduct)
        {
            if (id != warehouseProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warehouseProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarehouseProductExists(warehouseProduct.Id))
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
            return View(warehouseProduct);
        }

        // GET: WarehouseProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WarehouseProductTable == null)
            {
                return NotFound();
            }

            var warehouseProduct = await _context.WarehouseProductTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (warehouseProduct == null)
            {
                return NotFound();
            }

            return View(warehouseProduct);
        }

        // POST: WarehouseProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WarehouseProductTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.WarehouseProductTable'  is null.");
            }
            var warehouseProduct = await _context.WarehouseProductTable.FindAsync(id);
            if (warehouseProduct != null)
            {
                _context.WarehouseProductTable.Remove(warehouseProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarehouseProductExists(int id)
        {
          return (_context.WarehouseProductTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
         public IActionResult UserProducts()
        {
            List<UserProductVM> objProductsList = _context.WarehouseProductTable.Where(n => n.Warehouse_Id == contxt.HttpContext.Session.GetInt32("Warehouse")).Select(n => new UserProductVM
            {
                Id = n.Id,
                Warehouse_Id = n.Warehouse_Id,
                Product_Id  = n.Product_Id,
               Products = _context.ProductsTable.FirstOrDefault(p => p.Id == n.Product_Id),
               Warehouses = _context.WarehousesTable.FirstOrDefault(w => w.Id == n.Warehouse_Id)

            }).ToList();
            return View(objProductsList);
        }
    }
}
