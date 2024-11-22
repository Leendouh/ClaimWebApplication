using Microsoft.EntityFrameworkCore;

namespace ClaimWebApplication.ViewModel
{

        public class EditClaimViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
            public string EmployeeNo { get; set; }
            public string ContactNo { get; set; }
            public string Programme { get; set; }
            public string Module { get; set; }
            [Precision(18, 2)] // Precision: 18, Scale: 2
            public decimal HoursWorked { get; set; }
            [Precision(18, 2)] // Precision: 18, Scale: 2
            public decimal HourlyRate { get; set; }
            public IFormFile Document { get; set; } // For profile image upload
            public string URL { get; set; } // To hold the current image URL
        }

}
