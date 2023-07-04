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
    public class StockLevelsController : Controller
    {
        private readonly IHttpContextAccessor contxt;
        private readonly ApplicationDbContext _context;

        public StockLevelsController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            contxt = httpContextAccessor;
        }

        // GET: StockLevels
        public async Task<IActionResult> Index()
        {
            List<StockLevelVM> objStockLevel = _context.StockLevelTable.Select(n => new StockLevelVM{
                Id = n.Id,
                Quantity_In_Stock = n.Quantity_In_Stock,
                Product_Id = n.Product_Id,
                Warehouse_Id = n.Warehouse_Id,
                Products = _context.ProductsTable.FirstOrDefault(p => p.Id == n.Product_Id),
                Warehouses = _context.WarehousesTable.FirstOrDefault(w => w.Id == n.Warehouse_Id)
            }).ToList();
              return View(objStockLevel);
        }
        public async Task<IActionResult> IndexForUser()
        {
            List<StockLevelVM> objStockLevel = _context.StockLevelTable.Where(n => n.Warehouse_Id == contxt.HttpContext.Session.GetInt32("Warehouse")).Select(n => new StockLevelVM{
                Id = n.Id,
                Quantity_In_Stock = n.Quantity_In_Stock,
                Product_Id = n.Product_Id,
                Warehouse_Id = n.Warehouse_Id,
                Products = _context.ProductsTable.FirstOrDefault(p => p.Id == n.Product_Id),
                Warehouses = _context.WarehousesTable.FirstOrDefault(w => w.Id == n.Warehouse_Id)
            }).ToList();
              return View(objStockLevel);
        }

        // GET: StockLevels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockLevelTable == null)
            {
                return NotFound();
            }

            var stockLevel = await _context.StockLevelTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockLevel == null)
            {
                return NotFound();
            }

            return View(stockLevel);
        }

        // GET: StockLevels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StockLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Quantity_In_Stock,Product_Id,Warehouse_Id")] StockLevel stockLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stockLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockLevel);
        }

        // GET: StockLevels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StockLevelTable == null)
            {
                return NotFound();
            }

            var stockLevel = await _context.StockLevelTable.FindAsync(id);
            if (stockLevel == null)
            {
                return NotFound();
            }
            return View(stockLevel);
        }

        // POST: StockLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Quantity_In_Stock,Product_Id,Warehouse_Id")] StockLevel stockLevel)
        {
            if (id != stockLevel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockLevelExists(stockLevel.Id))
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
            return View(stockLevel);
        }

        // GET: StockLevels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockLevelTable == null)
            {
                return NotFound();
            }

            var stockLevel = await _context.StockLevelTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockLevel == null)
            {
                return NotFound();
            }

            return View(stockLevel);
        }

        // POST: StockLevels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockLevelTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StockLevelTable'  is null.");
            }
            var stockLevel = await _context.StockLevelTable.FindAsync(id);
            if (stockLevel != null)
            {
                _context.StockLevelTable.Remove(stockLevel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockLevelExists(int id)
        {
          return (_context.StockLevelTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
