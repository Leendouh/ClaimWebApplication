using ClaimWebApplication.Data;
using ClaimWebApplication.Models;
using ClaimWebApplication.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ClaimWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // Register GET action
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        // Register POST action
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string role)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.EmailAddress, Email = model.EmailAddress };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign role based on query string
                    if (role == UserRoles.Lecturer)
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.Lecturer);
                    }
                    else if (role == UserRoles.AcademicManager)
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.AcademicManager);
                    }
                    else if (role == UserRoles.ProgramCoordinator)
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.ProgramCoordinator);
                    }
                    else if (role == UserRoles.HumanRecources)
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.HumanRecources);
                    }

                    // Sign in the user after registration
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home"); // Redirect to home page or any other page
                }

                // If there are errors, add them to the ModelState
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        // Login GET action
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }

        // Login POST action
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string role)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.EmailAddress);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    if (result.Succeeded)
                    {
                        // Redirect based on the role from the query string
                        if (role == UserRoles.Lecturer)
                        {
                            return RedirectToAction("Claim", "Index");
                        }
                        else if (role == UserRoles.ProgramCoordinator)
                        {
                            return RedirectToAction("Claim", "Review");
                        }
                        else if (role == UserRoles.AcademicManager)
                        {
                            return RedirectToAction("Claim", "Details");
                        }
                        else if (role == UserRoles.HumanRecources)
                        {
                            return RedirectToAction("HR", "Index");
                        }

                        // Default redirect if no specific role matches
                        return RedirectToAction("Index", "Home");
                    }
                }

                // If login fails
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        // Logout action
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Sign out the current user
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); // Redirect after logout (or to any other page)
        }
    }
}
