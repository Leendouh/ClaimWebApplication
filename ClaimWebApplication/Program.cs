using ClaimWebApplication.Data;
using ClaimWebApplication.Interface;
using ClaimWebApplication.Models;
using ClaimWebApplication.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure Identity with EF Core
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

// Register the ClaimRepository as a service
builder.Services.AddScoped<IClaimRepository, ClaimRepository>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// wire up the dbcontext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // setting up our database by connecting to the sql server
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
var app = builder.Build();

// added code for the seed data
if (args.Length == 1 && args[0].ToLower() == "seeddata")
{
    // await Seed.SeedUserAndRolesAsync(app);
    Seed.SeedData(app);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Make sure authentication middleware is added
app.UseAuthentication(); 

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
