using System;
using System.IO;
using System.Reflection;
using System.Windows;

using ICSharpCode.Reporting;
using ICSharpCode.Reporting.Interfaces;
using ICSharpCode.Reporting.Pdf;
using ICSharpCode.Reporting.WpfReportViewer;

namespace ReportTest
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		
		void Button_Graphics(object sender, RoutedEventArgs e)
		{
			var stream = GraphicsContainer();
			
			var rf = new ReportingFactory();
			var reportCreator = rf.ReportCreator(stream);
			reportCreator.BuildExportList();		
			var previewViewModel = new PreviewViewModel (rf.ReportModel.ReportSettings,reportCreator.Pages);
			viewer.SetBinding(previewViewModel);
		}
		
		
		void Button_Plain(object sender, RoutedEventArgs e)
		{
			var stream = LoadPlainResource();
			
			var rf = new ReportingFactory();
			var reportCreator = rf.ReportCreator(stream);
			reportCreator.BuildExportList();		
			var previewViewModel = new PreviewViewModel (rf.ReportModel.ReportSettings,reportCreator.Pages);
			viewer.SetBinding(previewViewModel);
		}
		
		
		void Button_Small(object sender, RoutedEventArgs e)
		{
			var stream = LoadSmallResource();
			var cl = new ContributorsList();
			var cc = cl.SmallContributorCollection;
			
			var rf = new ReportingFactory();
			var reportCreator = rf.ReportCreator (stream,cc);
			reportCreator.BuildExportList();
			
			var previewViewModel = new PreviewViewModel (rf.ReportModel.ReportSettings,reportCreator.Pages);
			viewer.SetBinding(previewViewModel);
		}
		
		
		void Button_List(object sender, RoutedEventArgs e)
		{
			var stream = LoadListResource();
			
			var cl = new ContributorsList();
			var cc = cl.ContributorCollection;
			
			var rf = new ReportingFactory();
			var reportCreator = rf.ReportCreator (stream,cc);
			reportCreator.BuildExportList();
			var previewViewModel = new PreviewViewModel (rf.ReportModel.ReportSettings,reportCreator.Pages);
			viewer.SetBinding(previewViewModel);
		}
		
	#region Grouped
	
		void Grouped_List(object sender, RoutedEventArgs e)
		{
			var stream = LoadGroupedResource();
			
			var cl = new ContributorsList();
			var cc = cl.ContributorCollection;
			
			var rf = new ReportingFactory();
			var reportCreator = rf.ReportCreator (stream,cc);
			reportCreator.BuildExportList();
			
			var previewViewModel = new PreviewViewModel (rf.ReportModel.ReportSettings,reportCreator.Pages);
			viewer.SetBinding(previewViewModel);
		}
			
		void Grouped_To_Pdf(object sender, RoutedEventArgs e) {
			var stream = LoadGroupedResource();
			
			var cl = new ContributorsList();
			var cc = cl.ContributorCollection;
			
			var rf = new ReportingFactory();
			var reportCreator = rf.ReportCreator (stream,cc);
			reportCreator.BuildExportList();
			
			PdfExporter ex = new PdfExporter(reportCreator.Pages);
			ex.Run();
		}
	
		
		#endregion
		
		void Button_Pdf(object sender, RoutedEventArgs e) {
			var stream = LoadPlainResource();
			
			var rf = new ReportingFactory();
			var reportCreator = rf.ReportCreator(stream);
			reportCreator.BuildExportList();
			PdfExporter ex = new PdfExporter(reportCreator.Pages);
			ex.Run();
		}
		
		
		void List_To_Pdf(object sender, RoutedEventArgs e) {

			var stream = LoadListResource();
			
			var cl = new ContributorsList();
			var cc = cl.ContributorCollection;
			
			var rf = new ReportingFactory();
			var reportCreator = rf.ReportCreator (stream,cc);
			reportCreator.BuildExportList();
			PdfExporter ex = new PdfExporter(reportCreator.Pages);
			ex.Run();
		}
		
		
		Stream GraphicsContainer() {
			Assembly asm = Assembly.GetExecutingAssembly();
			var stream = asm.GetManifestResourceStream("ReportTest.Circle_text.srd");
			return stream;
		}
		
		Stream LoadPlainResource()
		{
			Assembly asm = Assembly.GetExecutingAssembly();
			var stream = asm.GetManifestResourceStream("ReportTest.ReportWithTwoItems.srd");
			return stream;
		}
		
		Stream LoadSmallResource()
		{
			Assembly asm = Assembly.GetExecutingAssembly();
			var stream = asm.GetManifestResourceStream("ReportTest.FromListNowRow.srd");
			return stream;
		}
		
		
		
		Stream LoadListResource()
		{
			Assembly asm = Assembly.GetExecutingAssembly();
			var stream = asm.GetManifestResourceStream("ReportTest.FromList.srd");
			return stream;
		}
		
		
		
		Stream LoadGroupedResource() {
			Assembly asm = Assembly.GetExecutingAssembly();
			var stream = asm.GetManifestResourceStream("ReportTest.GroupedList.srd");
			return stream;
		}
	
	}
}