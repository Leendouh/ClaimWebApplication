using Microsoft.EntityFrameworkCore;

namespace ClaimWebApplication.Models
{
    public class Claim
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public string EmployeeNo { get; set; }
        public string ContactNo { get; set; }
        public string Programme { get; set; }
        public string Module { get; set; }
        [Precision(18, 2)] // Precision: 18, Scale: 2
        public decimal HoursWorked { get; set; }
        [Precision(18, 2)] // Precision: 18, Scale: 2
        public decimal HourlyRate { get; set; }
        public DateOnly SubmissionDate { get; set; }
        public string SupportingDocs { get; set; }
        public string Status { get; set; } = "Pending"; // default status
        public decimal TotalPayment => HoursWorked * HourlyRate;

    }
}
