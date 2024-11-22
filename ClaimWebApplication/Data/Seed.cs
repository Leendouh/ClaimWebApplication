using ClaimWebApplication.Models;
using Microsoft.AspNetCore.Identity;


namespace ClaimWebApplication.Data
{
    public static class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                context.Database.EnsureCreated();

                // Seed Claims data if there are no existing claims
                if (!context.Claims.Any())
                {
                    context.Claims.AddRange(new List<Claim>()
                    {
                        new Claim()
                        {
                            EmployeeName = "Lindokuhle",
                            EmployeeSurname = "Zwane",
                            EmployeeNo = "L86-437-87",
                            ContactNo = "(+27) 67 543 9088",
                            Programme = "DISD0601",
                            Module = "PROG6212",
                            HoursWorked = 16,
                            HourlyRate = 3200.00m,
                            SubmissionDate = DateOnly.FromDateTime(DateTime.Now),
                            SupportingDocs = "/uploads/docs1.pdf",
                            Status = "Pending"
                        },
                        new Claim()
                        {
                            EmployeeName = "Daluxolo",
                            EmployeeSurname = "Zwide",
                            EmployeeNo = "L86-437-80",
                            ContactNo = "(+27) 79 765 8090",
                            Programme = "DINM0601",
                            Module = "WEDE6021",
                            HoursWorked = 12,
                            HourlyRate = 2400.00m,
                            SubmissionDate = DateOnly.FromDateTime(DateTime.Now),
                            SupportingDocs = "/uploads/Claim.pdf",
                            Status = "Approved"
                        }
                    });
                    context.SaveChanges();
                }

                // Seed Roles
                string[] roleNames = { "Lecturer", "AcademicManager", "ProgramCoordinator", "HumanResources" };

                foreach (var roleName in roleNames)
                {
                    var roleExist = roleManager.RoleExistsAsync(roleName).Result;
                    if (!roleExist)
                    {
                        roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
                    }
                }

                // Seed Users for each role
                CreateUserIfNotExist(userManager, "RC_lecturer@gmail.com", "Lecturer", "Lecturer123!");
                CreateUserIfNotExist(userManager, "RC_AM@gmail.com", "AcademicManager", "AcademicManager123!");
                CreateUserIfNotExist(userManager, "RC_PC@gmail.com", "ProgramCoordinator", "ProgramCoordinator123!");
                CreateUserIfNotExist(userManager, "RC_HR@gmail.com", "HumanResources", "HR123!");
            }
        }

        // Helper method to create a user if they don't exist
        private static void CreateUserIfNotExist(UserManager<AppUser> userManager, string email, string role, string password)
        {
            var user = userManager.FindByEmailAsync(email).Result;
            if (user == null)
            {
                user = new AppUser
                {
                    UserName = email,
                    Email = email,
                    FullName = "Default User" // Optional: Set FullName or other custom fields
                };
                var result = userManager.CreateAsync(user, password).Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, role).Wait();
                }
            }
        }

    }
}
