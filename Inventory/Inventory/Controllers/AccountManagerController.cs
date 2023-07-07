using Inventory.Data;
using Inventory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
	[Authorize]
	public class AccountManagerController : Controller
	{
		private readonly ApplicationDbContext _dbContext;

        public AccountManagerController(ApplicationDbContext dbContext)
        {
			_dbContext = dbContext;
		}
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

		public IActionResult SaveAccount(AccountManager account)
		{

			account.CreatingUser = User.Identity.Name;
			if (account.Id == 0) //add new Item if not editing
			{
				_dbContext.Accounts.Add(account);
				_dbContext.SaveChanges();
			}
			else
			{
				var accountFromDB = _dbContext.Accounts.SingleOrDefault(x => x.Id == account.Id);

				if (accountFromDB == null)
				{
					return NotFound();
				}

				accountFromDB.Name = account.Name;
				accountFromDB.Sirname = account.Sirname;
				accountFromDB.Username = account.Username;
				
			}

			_dbContext.SaveChanges();
			return RedirectToAction("ShowUser");
		}
	}
}
