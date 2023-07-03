using InventorySystemApplication.Models;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using InventorySystemApplication.Data;

namespace InventorySystemApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;


        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }
    
        public IActionResult Index()
        {
            SystemUser systemuser = new SystemUser();
            return View(systemuser);
        }
        [HttpPost]
        public IActionResult Index( SystemUser systemuser)
        {
            var userStatus = _context.SystemUsersTable.Where(m => m.User_Name == systemuser.User_Name && m.Password == systemuser.Password && m.UserType == "User").FirstOrDefault();
            var adminStatus = _context.SystemUsersTable.Where(m => m.User_Name == systemuser.User_Name && m.Password == systemuser.Password && m.UserType == "Admin").FirstOrDefault();
            if (userStatus == null && adminStatus == null)
            {
                ViewBag.LoginStatus = 0;
            }
            else if (userStatus == null && adminStatus != null)
            {
                return RedirectToAction(nameof(AdminMenu));
            }
            else if (userStatus != null && adminStatus == null)
            {
                foreach(var stat in userStatus){
                    TempData.Warehouse = stat.Warehouse_Id;
                    TempData.User = stat.Id;
                }
                return RedirectToAction(nameof(UserMenu));
            }

            return View(systemuser);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult AdminMenu()
        {
            return View();
        }
        public IActionResult UserMenu()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}