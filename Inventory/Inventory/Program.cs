using Inventory.Data;
using Inventory.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using static System.Collections.Specialized.BitVector32;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();
var serviceProvider = builder.Services.BuildServiceProvider();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
createRole(serviceProvider, "Admin").Wait();
CreateDefaultUser(serviceProvider, "Admin", "admin@inventory.com", "Test1.").Wait();
app.Run();

 async Task createRole(IServiceProvider serviceProvider, string rolename)
{


	var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

	var roleExits = await roleManager.RoleExistsAsync(rolename);

	if (roleExits)
		return;


	roleManager.CreateAsync(new IdentityRole(rolename));

}

 async Task CreateDefaultUser(IServiceProvider serviceProvider, string rolename, string username, string pw)
{
    var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();
    var user = await userManager.FindByNameAsync(username);

    if(user == null)
    {
        var newUser = new IdentityUser()
        {
            UserName = username,
            Email = username
        };

        userManager.CreateAsync(newUser, pw);
    }
	var allUser = await userManager.FindByNameAsync(username);

    await userManager.AddToRoleAsync(allUser, rolename);
}



