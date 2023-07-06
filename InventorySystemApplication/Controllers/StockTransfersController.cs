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
    public class StockTransfersController : Controller
    {
        private readonly IHttpContextAccessor contxt;
        private readonly ApplicationDbContext _context;

        public StockTransfersController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            contxt = httpContextAccessor;
        }

        // GET: StockTransfers
        public async Task<IActionResult> Index()
        {
            List<StockTfVM> objStockTf = _context.StockTransfersTable.Select(n => new StockTfVM{
                Id = n.Id,
                Product_Id = n.Product_Id,
                Warehouse_FromId = n.Warehouse_FromId,
                Warehouse_ToId = n.Warehouse_ToId,
                Quantity = n.Quantity,
                User_Id = n.User_Id,
                Transfer_Date = n.Transfer_Date,
                Document_Number = n.Document_Number,
                Products = _context.ProductsTable.FirstOrDefault(p => p.Id == n.Product_Id),
                WarehousesTo = _context.WarehousesTable.FirstOrDefault(w => w.Id == n.Warehouse_ToId),
                WarehousesFrom = _context.WarehousesTable.FirstOrDefault(w => w.Id == n.Warehouse_FromId),
                Users = _context.UsersTable.FirstOrDefault(u => u.Id == n.User_Id)
            }).ToList();
             return View(objStockTf);
        }
        public async Task<IActionResult> IndexForUser()
        {
            List<StockTfVM> objStockTf = _context.StockTransfersTable.Where(n => n.Warehouse_FromId == contxt.HttpContext.Session.GetInt32("Warehouse")).Select(n => new StockTfVM{
                Id = n.Id,
                Product_Id = n.Product_Id,
                Warehouse_FromId = n.Warehouse_FromId,
                Warehouse_ToId = n.Warehouse_ToId,
                Quantity = n.Quantity,
                User_Id = n.User_Id,
                Transfer_Date = n.Transfer_Date,
                Document_Number = n.Document_Number,
                Products = _context.ProductsTable.FirstOrDefault(p => p.Id == n.Product_Id),
                WarehousesTo = _context.WarehousesTable.FirstOrDefault(w => w.Id == n.Warehouse_ToId),
                WarehousesFrom = _context.WarehousesTable.FirstOrDefault(w => w.Id == n.Warehouse_FromId),
                Users = _context.UsersTable.FirstOrDefault(u => u.Id == n.User_Id)
            }).ToList();
             return View(objStockTf);
        }

        // GET: StockTransfers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StockTransfersTable == null)
            {
                return NotFound();
            }

            var stockTransfer = await _context.StockTransfersTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockTransfer == null)
            {
                return NotFound();
            }

            return View(stockTransfer);
        }

        // GET: StockTransfers/Create
        public IActionResult Create()
        {
            var prod = _context.ProductsTable.ToList();
            ViewBag.Products = prod;
            var war = _context.WarehousesTable.ToList();
            ViewBag.Warehouses = war;
            var use = _context.UsersTable.ToList();
            ViewBag.Users = use;
            return View();
        }

        // POST: StockTransfers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Product_Id,Warehouse_FromId,Warehouse_ToId,Quantity,User_Id,Transfer_Date,Document_Number")] StockTransfer stockTransfer)
        {
           if (ModelState.IsValid)
            {
                stockTransfer.Warehouse_FromId = (int)contxt.HttpContext.Session.GetInt32("Warehouse");
                stockTransfer.User_Id = (int)contxt.HttpContext.Session.GetInt32("User");
                List<StockLevel> stocklevlObj = _context.StockLevelTable.ToList();
                List<WarehouseProduct> warehouseProductObj = _context.WarehouseProductTable.ToList();
                int exists = 0;
                foreach (var warehouseProduct in warehouseProductObj)
                {
                    if (warehouseProduct.Warehouse_Id == stockTransfer.Warehouse_FromId && warehouseProduct.Product_Id == stockTransfer.Product_Id)
                    {
                        exists = 1;
                        break;
                    }
                    exists = 0;
                }
                if (exists == 0)
                {
                    ModelState.AddModelError("Warehouse_FromId", "Product does not exist in this warehouse");
                    return View();
                }

                if (stocklevlObj.Count == 0)
                {
                    ModelState.AddModelError("Warehouse_FromId", "Warehouse has no Product");
                    return View();
                }
                int sufficient = 0;
                foreach (var stock in stocklevlObj)
                {
                    if (stock.Product_Id == stockTransfer.Product_Id && stock.Warehouse_Id == stockTransfer.Warehouse_FromId)
                    {

                        if (stockTransfer.Quantity > stock.Quantity_In_Stock)
                        {
                            ModelState.AddModelError("Quantity", "Insufficient Product");
                            return View();

                        };
                        if (stock.Quantity_In_Stock >= stockTransfer.Quantity)
                        {
                            sufficient = 1;
                            break;
                        }

                    }
                }


                int addable = 0;
                foreach (var stock in stocklevlObj)
                {
                    if (stock.Warehouse_Id == stockTransfer.Warehouse_ToId)
                    {
                        stock.Quantity_In_Stock += stockTransfer.Quantity;
                        _context.StockLevelTable.Update(stock);
                        addable = 1;
                        break;
                    }
                    addable = 0;
                }


                if (addable == 0)
                {
                    StockLevel stocklevel = new()
                    {
                        Quantity_In_Stock = stockTransfer.Quantity,
                        Product_Id = stockTransfer.Product_Id,
                        Warehouse_Id = stockTransfer.Warehouse_ToId
                    };
                    _context.StockLevelTable.Add(stocklevel);

                }


                int wpv = 0;
                foreach (var wp in warehouseProductObj)
                {
                    if (wp.Product_Id == stockTransfer.Product_Id && wp.Warehouse_Id == stockTransfer.Warehouse_ToId)
                    {
                        wpv = 1;
                        break;
                    }
                    wpv = 0;
                }

                if (wpv == 0)
                {
                    WarehouseProduct warehouseProduct = new()
                    {
                        Warehouse_Id = stockTransfer.Warehouse_ToId,
                        Product_Id = stockTransfer.Product_Id
                    };
                    _context.WarehouseProductTable.Add(warehouseProduct);
                }

                foreach (var stock in stocklevlObj)
                {
                    if (stock.Warehouse_Id == stockTransfer.Warehouse_FromId)
                    {
                        stock.Quantity_In_Stock -= stockTransfer.Quantity;
                        _context.StockLevelTable.Update(stock);
                        _context.SaveChanges();
                        break;
                    }

                }
                var secStockObj = _context.StockLevelTable.Where(n => n.Warehouse_Id == stockTransfer.Warehouse_Id && n.Product_Id == stockTransfer.Product_Id).FirstOrDefault();
                

                if(secStockObj.Quantity_In_Stock == 0){
                    foreach (var wp in warehouseProductObj)
                                {
                                    if (wp.Product_Id == stockTransfer.Product_Id && wp.Warehouse_Id == stockTransfer.Warehouse_FromId)
                                    {
                                        _context.WarehouseProductTable.Remove(wp);
                                    }

                                }
                }
                


                if (exists == 1 && sufficient == 1)
                {
                    _context.StockTransfersTable.Add(stockTransfer);
                    _context.SaveChanges();


                }

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: StockTransfers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StockTransfersTable == null)
            {
                return NotFound();
            }

            var stockTransfer = await _context.StockTransfersTable.FindAsync(id);
            if (stockTransfer == null)
            {
                return NotFound();
            }
            return View(stockTransfer);
        }

        // POST: StockTransfers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Product_Id,Warehouse_FromId,Warehouse_ToId,Quantity,User_Id,Transfer_Date,Document_Number")] StockTransfer stockTransfer)
        {
            if (id != stockTransfer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stockTransfer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StockTransferExists(stockTransfer.Id))
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
            return View(stockTransfer);
        }

        // GET: StockTransfers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StockTransfersTable == null)
            {
                return NotFound();
            }

            var stockTransfer = await _context.StockTransfersTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stockTransfer == null)
            {
                return NotFound();
            }

            return View(stockTransfer);
        }

        // POST: StockTransfers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StockTransfersTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.StockTransfersTable'  is null.");
            }
            var stockTransfer = await _context.StockTransfersTable.FindAsync(id);
            if (stockTransfer != null)
            {
                _context.StockTransfersTable.Remove(stockTransfer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StockTransferExists(int id)
        {
          return (_context.StockTransfersTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
