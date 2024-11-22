using ClaimWebApplication.Interface;
using ClaimWebApplication.Models;
using ClaimWebApplication.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimWebApplication.Controllers
{
    public class HRController : Controller
    {
        private readonly IClaimRepository _claimRepository;

        public HRController(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }
        //[Authorize(Roles = "HumanResources")]
        public async Task<IActionResult> Index()
        {
            List<Claim> claims = await _claimRepository.GetAllClaimsAsync();
            return View(claims);
        }
        public async Task<IActionResult> Review(int id)
        {
            var claim = await _claimRepository.GetClaimByIdAsync(id);
            return View(claim);
        }
        //[Authorize(Roles = "HumanResources")]
        [HttpGet]
        public async Task<IActionResult> GenerateReport(int id)
        {
            // Fetch the specific claim by ID
            var claim = await _claimRepository.GetClaimByIdAsync(id);

            if (claim == null)
            {
                return NotFound("Claim not found");
            }

            // Generate the report for the specific claim
            var reportBytes = ReportGenerator.GenerateClaimReport(claim);

            // Return the report as a downloadable file
            return File(reportBytes, "application/pdf", $"ClaimReport_{id}.pdf");
        }
        // GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            var claim = await _claimRepository.GetClaimByIdAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            return View(claim); // Return claim for editing in the view
        }

        // POST: Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Claim model, IFormFile supportingDocs)
        {
            if (id != model.ID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var claim = await _claimRepository.GetClaimByIdAsync(id);
                if (claim == null)
                {
                    return NotFound();
                }

                // Update the claim with only the editable fields
                claim.Programme = model.Programme;
                claim.Module = model.Module;
                claim.HoursWorked = model.HoursWorked;
                claim.HourlyRate = model.HourlyRate;

                // Handle supporting docs update if provided
                if (supportingDocs != null && supportingDocs.Length > 0)
                {
                    var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadFolderPath))
                    {
                        Directory.CreateDirectory(uploadFolderPath);
                    }

                    var filePath = Path.Combine(uploadFolderPath, supportingDocs.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await supportingDocs.CopyToAsync(stream);
                    }

                    claim.SupportingDocs = "/uploads/" + supportingDocs.FileName;
                }

                // Save the updated claim
                await _claimRepository.UpdateClaimAsync(claim);

                // Redirect to the Details page after update
                return RedirectToAction("Details");
            }

            return View(model); // Return the view with validation errors
        }
    }
}
