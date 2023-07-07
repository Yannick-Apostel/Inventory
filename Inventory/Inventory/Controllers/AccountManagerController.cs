using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
	public class AccountManagerController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult CreateUser()
		{
			return View();
		}

		public IActionResult ShowUser()
		{
			return View();
		}

		public IActionResult Settings()
		{
			return View();
		}
	}
}
