using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using ICSharpCode.Reporting;
using ICSharpCode.Reporting.Pdf;
using WebAppMvc.Samples;

namespace WebAppMvc.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Home/ContributorsList/
        public ActionResult ContributorsList()
        {
            var contributors = new ContributorsList().SmallContributorCollection;

            var reportDefinitionAsStream = LoadStreamFromResource("FromListNowRow.srd");

            var rf = new ReportingFactory();
            var reportCreator = rf.ReportCreator(reportDefinitionAsStream, contributors);
            reportCreator.BuildExportList();

            var ms = new MemoryStream();
            PdfExporter ex = new PdfExporter(reportCreator.Pages);
            ex.Run(ms);

            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/pdf", "contributors.pdf");
        }

        private static Stream LoadStreamFromResource(string resourceName)
        {
            var asm = Assembly.GetAssembly(typeof(HomeController));
            return asm.GetManifestResourceStream("WebAppMvc.Samples." + resourceName);
        }
    }
}