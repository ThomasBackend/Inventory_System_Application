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
    public class StockInsController : Controller
    {
        private readonly IHttpContextAccessor contxt;
        private readonly ApplicationDbContext _context;

        public StockInsController(ApplicationDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            contxt = httpContextAccessor;
        }
        // GET: StockIns
        public async Task<IActionResult> Index()
        {
            List<StockInVM> objStockIn = _context.StockInTable.Select(n => new StockInVM{
                Id = n.Id,
                Product_Id = n.Product_Id,
                Quantity_In = n.Quantity_In,
                Warehouse_Id = n.Warehouse_Id,
                Date_In = n.Date_In,
                User_Id = n.User_Id,
                Document_Number = n.Document_Number,
                Products = _context.ProductsTable.FirstOrDefault(p => p.Id == n.Product_Id),
                Warehouses = _context.WarehousesTable.FirstOrDefault(w => w.Id == n.Warehouse_Id),
                Users = _context.UsersTable.FirstOrDefault(u => u.Id == n.User_Id)
            }).ToList();
              return View(objStockIn);
        }
         
         public async Task<IActionResult> IndexForUser()
        {
            List<StockInVM> objStockIn = _context.StockInTable.Where(n => n.Warehouse_Id == contxt.HttpContext.Session.GetInt32("Warehouse")).Select(n => new StockInVM{
                Id = n.Id,
                Product_Id = n.Product_Id,
                Quantity_In = n.Quantity_In,
                Warehouse_Id = n.Warehouse_Id,
                Date_In = n.Date_In,
                User_Id = n.User_Id,
                Document_Number = n.Document_Number,
                Products = _context.ProductsTable.FirstOrDefault(p => p.Id == n.Product_Id),
                Warehouses = _context.WarehousesTable.FirstOrDefault(w => w.Id == n.Warehouse_Id),
                Users = _context.UsersTable.FirstOrDefault(u => u.Id == n.User_Id)
            }).ToList();
              return View(objStockIn);
        }
        // GET: StockIns/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockInTable == null)
            {
                return NotFound();
            }

            var stockIn = await _context.StockInTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockIn == null)
            {
                return NotFound();
            }

            return View(stockIn);
        }

        // GET: StockIns/Create
        public IActionResult Create()
        {
            var prod = _context.ProductsTable.ToList();
            ViewBag.Products = prod;
            var use = _context.UsersTable.ToList();
            ViewBag.Users = use;
            var war = _context.WarehousesTable.ToList();
            ViewBag.Warehouses = war;
            return View();
        }

        // POST: StockIns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Product_Id,Quantity_In,Warehouse_Id,Date_In,User_Id,Document_Number")] StockIn stockIn)
        {
            bool empty = true;
            if (ModelState.IsValid)
            {
                stockIn.Warehouse_Id = (int)contxt.HttpContext.Session.GetInt32("Warehouse") ;
                stockIn.User_Id = (int)contxt.HttpContext.Session.GetInt32("User") ;
            List<StockLevel> stocklevlObj = _context.StockLevelTable.ToList();
            List<WarehouseProduct> wpObj = _context.WarehouseProductTable.ToList();
            
            if(stocklevlObj != null){
                empty = false;
            }
            if(empty != true){
            foreach(var stock in stocklevlObj){
            if(stock.Warehouse_Id == stockIn.Warehouse_Id && stock.Product_Id == stockIn.Product_Id){

                stock.Quantity_In_Stock += stockIn.Quantity_In;
                _context.StockLevelTable.Update(stock);
                empty = false;
                break;
            }
            empty = true;
            }}

            if(empty = true){
                StockLevel stocklevel = new()
                    {
                        Quantity_In_Stock = stockIn.Quantity_In,
                        Product_Id = stockIn.Product_Id,
                        Warehouse_Id = stockIn.Warehouse_Id
                    };
                    _context.StockLevelTable.Add(stocklevel);
                    
            }

            bool exists = false;
            foreach(var wp in wpObj){
                if(wp.Product_Id == stockIn.Product_Id && wp.Warehouse_Id == stockIn.Warehouse_Id){
                    exists = true;
                    break;
                }
                exists = false;
            }
            if(exists != true){
                WarehouseProduct warehouseproduct = new(){
                Product_Id = stockIn.Product_Id,
                Warehouse_Id = stockIn.Warehouse_Id
                };
                _context.WarehouseProductTable.Add(warehouseproduct);
            }



                _context.Add(stockIn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockIn);
        }

        // GET: StockIns/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StockInTable == null)
            {
                return NotFound();
            }

            var stockIn = await _context.StockInTable.FindAsync(id);
            if (stockIn == null)
            {
                return NotFound();
            }
            return View(stockIn);
        }

        // POST: StockIns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Product_Id,Quantity_In,Warehouse_Id,Date_In,User_Id,Document_Number")] StockIn stockIn)
        {
            if (id != stockIn.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockIn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockInExists(stockIn.Id))
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
            return View(stockIn);
        }

        // GET: StockIns/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockInTable == null)
            {
                return NotFound();
            }

            var stockIn = await _context.StockInTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockIn == null)
            {
                return NotFound();
            }

            return View(stockIn);
        }

        // POST: StockIns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockInTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StockInTable'  is null.");
            }
            var stockIn = await _context.StockInTable.FindAsync(id);
            if (stockIn != null)
            {
                _context.StockInTable.Remove(stockIn);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockInExists(int id)
        {
          return (_context.StockInTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
