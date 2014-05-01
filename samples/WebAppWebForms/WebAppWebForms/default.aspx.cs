using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ICSharpCode.Reporting;
using ICSharpCode.Reporting.Pdf;

namespace WebAppWebForms
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void createPdf(object sender, EventArgs e)
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

            Response.ContentType = "application/pdf";
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }

        private static Stream LoadStreamFromResource(string resourceName)
        {
            var asm = Assembly.GetAssembly(typeof(_default));
            return asm.GetManifestResourceStream("WebAppWebForms." + resourceName);
        }
    }
}