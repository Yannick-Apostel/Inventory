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

		public IActionResult ShowUser()
		{
		var allUsersInDB = _dbContext.Accounts.ToList();
			return View(allUsersInDB);
		}

		public IActionResult Settings()
		{
			return View();
		}

		public IActionResult CreateEditUser(int id)
		{
			if (id != 0)
			{
				var accountFromDb = _dbContext.Accounts.SingleOrDefault(x => x.Id == id);

				

				if (accountFromDb != null)
				{
					return View(accountFromDb);
				}
				else
				{
					return RedirectToAction("CreateEditUser");
				}
			}
			return View();
		}

		public IActionResult SaveAccount(AccountManager account)
		{

			
			if (account.Id == 0 && CheckExitsAccount(account) == false) //add new Item if not editing
			{
				_dbContext.Accounts.Add(account);
				account.CreatingUser = User.Identity.Name;
			}
			else
			{

				var accountFromDB = _dbContext.Accounts.SingleOrDefault(x => x.Username == account.Username);

				if (accountFromDB == null)
				return NotFound();
				

				accountFromDB.Name = account.Name;
				accountFromDB.Sirname = account.Sirname;
				accountFromDB.Username = account.Username;
				
				
			}

			_dbContext.SaveChanges();
			return RedirectToAction("ShowUser");
		}

		public bool CheckExitsAccount(AccountManager account) 
		{
			foreach (AccountManager accountInDB in _dbContext.Accounts) 
			{
				if (accountInDB.Username == account.Username)
					return true;
				
			}
			return false;
		}

		[HttpPost]
		public IActionResult DeleteAccountById(int id) 
		{
			if (id == 0)
				return BadRequest();

			var accountFromDb = _dbContext.Accounts.SingleOrDefault(x => x.Id == id);

			if (accountFromDb == null)
				return NotFound();

			_dbContext.Accounts.Remove(accountFromDb);
			_dbContext.SaveChanges();

			return Ok();
			
		} 
	}
}
