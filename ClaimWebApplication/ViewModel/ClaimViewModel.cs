namespace ClaimWebApplication.ViewModel
{
    public class ClaimViewModel
    {
        public int ID { get; set; }
        public string EmployeeName { get; set; } // Add this property
        public string EmployeeSurname { get; set; } // Add this property
        public string EmployeeFullName => $"{EmployeeName} {EmployeeSurname}";
        public string EmployeeNo { get; set; }
        public string Programme { get; set; }
        public string Module { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TotalPayment { get; set; }
        public string Status { get; set; }
        public string SupportingDocs { get; set; }
        public DateOnly SubmissionDate { get; set; }
        public string StatusClass => Status == "Approved" ? "text-success" : Status == "Rejected" ? "text-danger" : "text-warning";
    }
}
