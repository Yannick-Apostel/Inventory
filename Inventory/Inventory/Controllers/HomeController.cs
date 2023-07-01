using Inventory.Data;
using Inventory.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Inventory.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {

            _logger = logger;
            _dbContext = dbContext;
			var builder = WebApplication.CreateBuilder();
			var serviceProvider = builder.Services.BuildServiceProvider();
            createRole(serviceProvider, GetUserName());

		}
        public List<IdentityUser> GetUserName()
        {
            List<IdentityUser> users = new List<IdentityUser>();
            users = _dbContext.Users.ToList();
            return users;
        }

        public IActionResult Index()
        {
			var itemFromDb = _dbContext.Components.Where(x => x.OwnerUsername == User.Identity.Name).ToList();
			return View(itemFromDb);
			
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult GetItemPartial(string query)
        {
            List<Item> items = new List<Item>();

            if (string.IsNullOrWhiteSpace(query))
                items = _dbContext.Components.Where(x => x.OwnerUsername == User.Identity.Name).ToList();
			else
                items = _dbContext.Components
                    .Where(x => x.Name.ToLower().Contains(query.ToLower()) &&  x.OwnerUsername == User.Identity.Name)
                    .ToList();

           
                
            return PartialView("_ItemListPartial", items);
        }

		public async Task createRole(IServiceProvider serviceProvider, List<IdentityUser> users)
		{


			//foreach (IdentityUser user in users)
			//{

			//	var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

			//	var roleExits = await roleManager.RoleExistsAsync(user.UserName);

			//	if (roleExits)
			//		return;

			//	await roleManager.CreateAsync(new IdentityRole(user.UserName));
			//}

		}
	}
}