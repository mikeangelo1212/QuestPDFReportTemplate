using PDFGenerator.Models;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


// TODO: set your license here:
QuestPDF.Settings.License = LicenseType.Evaluation;

var document = new Report(
    new ReportData
    {
        Title ="titulo",
        IssueDate =new DateOnly(2026,06,17),
        DateStart=new DateOnly(2023,11,28),
        DateEnd=new DateOnly(2025,06,17),
        Comments="",
        ColumnsAndRows = new List<List<string>>(),
        HasTotal =false,
        TotalColumn=null
    }
);

// instead of the standard way of generating a PDF file
document.GeneratePdf("hello.pdf");

// use the following invocation
document.ShowInCompanion();

// optionally, you can specify an HTTP port to communicate with the previewer host (default is 12500)
document.ShowInCompanion(12500);