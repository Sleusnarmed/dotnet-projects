using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using dotnet_projects.Data;
using dotnet_projects.Models;

namespace dotnet_projects.Controllers
{
    public class TestController : Controller
    {
        private readonly AppDbContext _context;

        public TestController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {

                var canConnect = await _context.Database.CanConnectAsync();
                ViewBag.CanConnect = canConnect;
                
                if (canConnect)
                {

                    var userCount = await _context.Users.CountAsync();
                    ViewBag.UserCount = userCount;
                    
                    var inventoryCount = await _context.Inventories.CountAsync();
                    ViewBag.InventoryCount = inventoryCount;
                    
                    var itemCount = await _context.InventoryItems.CountAsync();
                    ViewBag.ItemCount = itemCount;
                    
                    ViewBag.Users = await _context.Users.ToListAsync();
                    ViewBag.Inventories = await _context.Inventories.Include(i => i.Owner).ToListAsync();
                    ViewBag.Items = await _context.InventoryItems.Include(i => i.Inventory).ToListAsync();
                }
                
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }
    }
}