using ClaimWebApplication.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace ClaimWebApplication.Utilities
{
    public static class ReportGenerator
    {
        public static byte[] GenerateClaimReport(Claim claim)
        {
            using (var stream = new MemoryStream())
            {
                Document document = new Document();
                PdfWriter.GetInstance(document, stream);
                document.Open();

                // Add a title
                document.Add(new Paragraph("Claim Report", FontFactory.GetFont("Arial", 16, Font.BOLD)));

                // Create a table with 2 columns for the claim details
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100; // Set the table to occupy the full width of the page

                // Add table headers
                table.AddCell("Field");
                table.AddCell("Value");

                // Add claim details in table format
                table.AddCell("Claim ID");
                table.AddCell(claim.ID.ToString());

                table.AddCell("Employee Name");
                table.AddCell($"{claim.EmployeeName} {claim.EmployeeSurname}");

                table.AddCell("Programme");
                table.AddCell(claim.Programme);

                table.AddCell("Module");
                table.AddCell(claim.Module);

                table.AddCell("Total Payment");
                table.AddCell(claim.TotalPayment.ToString("C"));

                table.AddCell("Status");
                table.AddCell(claim.Status);

                table.AddCell("Submission Date");
                table.AddCell(claim.SubmissionDate.ToString("dd MMM yyyy"));

                // Add the table to the document
                document.Add(table);

                // Close the document
                document.Close();

                return stream.ToArray();
            }
        }
    }
}
