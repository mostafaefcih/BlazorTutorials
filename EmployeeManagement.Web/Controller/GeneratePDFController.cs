using EmployeeManagement.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Web.Controller
{
    public class GeneratePDFController : ControllerBase
    {
        //private readonly IWebHostEnvironment _webHostEnvironment;
        [HttpGet]
        [Route("DownloadPDFFile")]
        public IActionResult DownloadPDFFile(string pageName)
        {

            var pdf = new GeneratePdf($"https://{Request.Host.Value}/{pageName}");

            var pdfFile = pdf.GetPdf();
            var pdfStream = new System.IO.MemoryStream(pdfFile);
            return new FileStreamResult(pdfStream, "application/pdf");

        }
    }
}
