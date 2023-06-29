using Inventory.Data;
using Inventory.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public ItemController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateEditItem(int id)
        {
            return View();
        }
        public IActionResult SaveItem(Item item)
        {
            
            if(item.Id == 0) //add new Item if not editing
            {
            _dbContext.Components.Add(item);
            _dbContext.SaveChanges();
            }
            else
            {
                var itemFromDb = _dbContext.Components.SingleOrDefault(x => x.Id == item.Id);
                
                if(itemFromDb == null)
                {
                    return NotFound();
                }

                itemFromDb.Name = item.Name;
                itemFromDb.count = item.count;
                itemFromDb.Description = item.Description;
            }
            

            return RedirectToAction("Index");
        }
    }
}
