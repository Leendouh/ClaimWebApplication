using ClaimWebApplication.Data;
using Microsoft.AspNetCore.Mvc;


namespace ClaimWebApplication.Controllers
{
    public class ReviewClaimController : Controller
    {
        private readonly ApplicationDbContext _context;

        // a constructor to include our database 
        public ReviewClaimController(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}
