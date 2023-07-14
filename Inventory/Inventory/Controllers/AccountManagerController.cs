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
		var allUsersInDB = _dbContext.Accounts.ToList();
			return View(allUsersInDB);
		}

		public IActionResult Settings()
		{
			return View();
		}

		public IActionResult SaveAccount(AccountManager account)
		{

			
			if (account.Id == 0 && CheckExitsAccount(account) == false) //add new Item if not editing
			{
				_dbContext.Accounts.Add(account);

			}
			else
			{

				var accountFromDB = _dbContext.Accounts.SingleOrDefault(x => x.Username == account.Username);

				if (accountFromDB == null)
				{
					return NotFound();
				}

				accountFromDB.Name = account.Name;
				accountFromDB.Sirname = account.Sirname;
				accountFromDB.Username = account.Username;
				account.CreatingUser = User.Identity.Name;
				
			}

			_dbContext.SaveChanges();
			return RedirectToAction("ShowUser");
		}

		public bool CheckExitsAccount(AccountManager account) 
		{
			foreach (AccountManager accountInDB in _dbContext.Accounts) 
			{
				if (accountInDB.Username == account.Username)
				{
					return true;
				}
			}
			return false;
		}
	}
}
