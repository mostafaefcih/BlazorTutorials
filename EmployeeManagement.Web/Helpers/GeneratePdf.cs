using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Web.Helpers
{
    public class GeneratePdf
    {
        private string Url;
        public GeneratePdf(string _url)
        {
            this.Url = _url;
        }
        // just test rotative it's convert html to pdf
        public byte[] GetPdf()
        {
            var switchs = $"-q {Url} -";
            string rotativePath =
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "Rotativa",
                "wkhtmltopdf.exe"
                );
            using (var proc = new Process())
            {
                try
                {
                    proc.StartInfo = new ProcessStartInfo()
                    {
                        FileName = rotativePath,
                        Arguments = switchs,
                        UseShellExecute = false,
                        RedirectStandardOutput = true,
                        RedirectStandardInput = true,
                        RedirectStandardError = true,
                        CreateNoWindow = true,
                    };
                    proc.Start();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                using (var ms = new MemoryStream())
                {
                    proc.StandardOutput.BaseStream.CopyTo(ms);
                    return ms.ToArray();
                }

            }
        }

    }
}
