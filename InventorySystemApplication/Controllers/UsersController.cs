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
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            List<UserVM> objUser = _context.UsersTable.Select(n => new UserVM{
                Id = n.Id,
                User_Name = n.User_Name,
                User_Email = n.User_Email,
                User_Telephone = n.User_Telephone,
                Warehouse_Id = n.Warehouse_Id,
                Password = n.Password,
                Warehouses = _context.WarehousesTable.FirstOrDefault(w => w.Id == n.Warehouse_Id)
            }).ToList(); 
              return View(objUser);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.UsersTable == null)
            {
                return NotFound();
            }

            var user = await _context.UsersTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var war = _context.WarehousesTable.ToList();
            ViewBag.Warehouses = war;
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,User_Name,User_Email,User_Telephone,Warehouse_Id,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                SystemUser systemuser = new SystemUser {
                User_Id = user.Id,
                User_Name = user.User_Name ,
                User_Email = user.User_Email ,
                User_Telephone = user.User_Telephone ,
                Warehouse_Id = user.Warehouse_Id ,
                Password = user.Password
                };
                _context.SystemUsersTable.Add(systemuser);
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.UsersTable == null)
            {
                return NotFound();
            }

            var user = await _context.UsersTable.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var war = _context.WarehousesTable.ToList();
            ViewBag.Warehouses = war;
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,User_Name,User_Email,User_Telephone,Warehouse_Id")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    List<SystemUser> systemUserObj = _context.SystemUsersTable.ToList();
                    if(systemUserObj != null)
                    {
                        foreach(var obj in systemUserObj)
                        {
                            if(obj.User_Id == user.Id)
                            {
                                obj.User_Name = user.User_Name;
                                obj.User_Email = user.User_Email;
                                obj.User_Telephone = user.User_Telephone;
                                obj.Warehouse_Id = user.Warehouse_Id;
                                obj.Password = user.Password;
                                
                                _context.SystemUsersTable.Update(obj);
                                break;
                            }; ;
                        }
                    }
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.UsersTable == null)
            {
                return NotFound();
            }

            var user = await _context.UsersTable
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.UsersTable == null)
            {
                return Problem("Entity set 'ApplicationDbContext.UsersTable'  is null.");
            }
            var user = await _context.UsersTable.FindAsync(id);
            if (user != null)
            {
                _context.UsersTable.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.UsersTable?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
