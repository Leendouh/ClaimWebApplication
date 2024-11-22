using ClaimWebApplication.Interface;
using ClaimWebApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ClaimWebApplication.Controllers
{
    public class ClaimController : Controller
    {
        private readonly IClaimRepository _claimRepository;

        public ClaimController(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public IActionResult SubmitClaim()
        {
            return View();
        }

        //[Authorize(Roles = "Lecturer")]
        public async Task<IActionResult> Index()
        {
            List<Claim> claims = await _claimRepository.GetAllClaimsAsync();
            return View(claims);
        }

        //[Authorize(Roles = "ProgramCoordinator")]
        public async Task<IActionResult> Review(int id)
        {
            var claim = await _claimRepository.GetClaimByIdAsync(id);
            return View(claim);
        }
        //[Authorize(Roles = "ProgramCoordinator")]
        public async Task<IActionResult> Confirm()
        {
            List<Claim> claims = await _claimRepository.GetAllClaimsAsync();
            return View(claims);
        }

        //[Authorize(Roles = "Lecturer")]
        [HttpPost]
        public async Task<IActionResult> SubmitClaim(Claim model, IFormFile supportingDocs)
        {
            if (supportingDocs != null && supportingDocs.Length > 0)
            {
                // Define the upload folder path
                var uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                // Create the directory if it doesn't exist
                if (!Directory.Exists(uploadFolderPath))
                {
                    Directory.CreateDirectory(uploadFolderPath);
                }

                // Define the file path where the document will be saved
                var filePath = Path.Combine(uploadFolderPath, supportingDocs.FileName);

                // Save the file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await supportingDocs.CopyToAsync(stream);
                }

                // Store the relative file path in the model (the relative path for the document in the 'uploads' folder)
                model.SupportingDocs = "/uploads/" + supportingDocs.FileName;
            }

            // Add the claim to the repository(assumed to save the claim data)
            await _claimRepository.AddClaimAsync(model);

            // Redirect to the Index page after the claim is submitted
            return RedirectToAction("Index");
        }

        //[Authorize(Roles = "AcademicManager")]
        public async Task<IActionResult> Details()
        {
            var claims = await _claimRepository.GetPendingClaimsAsync();
            return View(claims);
        }

        [HttpPost]
        public async Task<IActionResult> ApproveClaim(int claimId)
        {
            var claim = await _claimRepository.GetClaimByIdAsync(claimId);
            if (claim != null)
            {
                claim.Status = "Approved";
                await _claimRepository.UpdateClaimAsync(claim);
            }
            return RedirectToAction("Details");
        }

        [HttpPost]
        public async Task<IActionResult> RejectClaim(int claimId)
        {
            var claim = await _claimRepository.GetClaimByIdAsync(claimId);
            if (claim != null)
            {
                claim.Status = "Rejected";
                await _claimRepository.UpdateClaimAsync(claim);
            }
            return RedirectToAction("Details");
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
