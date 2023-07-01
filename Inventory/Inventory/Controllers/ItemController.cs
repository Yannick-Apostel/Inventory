using Inventory.Data;
using Inventory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public ItemController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var itemFromDb = _dbContext.Components.Where(x => x.OwnerUsername == User.Identity.Name).ToList();
            return View(itemFromDb);
			

		}

        public IActionResult CreateEditItem(int id)
        {
          

            if (id != 0)
            {
                var itemFromDb = _dbContext.Components.SingleOrDefault(x => x.Id == id);

                if (itemFromDb.OwnerUsername != User.Identity.Name)
                    return Unauthorized();
               
                if (itemFromDb != null)
                {
                    return View(itemFromDb);
                }
                else
                {
                    return NotFound();
                }
            }
            return View();
        }
        public IActionResult SaveItem(Item item)
        {
            item.OwnerUsername = User.Identity.Name;
            
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
                itemFromDb.OwnerUsername = item.OwnerUsername;
            }

            _dbContext.SaveChanges();
            

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteItemById(int id) 
        {
            if (id == 0)
                return BadRequest();

            var itemFromDB = _dbContext.Components.SingleOrDefault(x =>x.Id == id);

            if(itemFromDB == null)
             return NotFound(); 

            _dbContext.Components.Remove(itemFromDB);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetItem(int id)
        {
            if (id == 0)
                return BadRequest();

            var itemFromDb = _dbContext.Components.SingleOrDefault(x =>x.Id == id);

            if(itemFromDb == null)
                return NotFound();

            return Ok(itemFromDb);
        }
		



	}
}
