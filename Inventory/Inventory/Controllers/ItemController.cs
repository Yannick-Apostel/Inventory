using Inventory.Models;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    public class ItemController : Controller
    {
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
            //ToDo write Item to DB

            return RedirectToAction("Index");
        }
    }
}
