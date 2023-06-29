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
    }
}
