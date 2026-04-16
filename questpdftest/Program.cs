using PDFGenerator.Models;
using QuestPDF.Companion;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;


// TODO: set your license here:
QuestPDF.Settings.License = LicenseType.Evaluation;

var listaEjemplo= new List<List<string>>{
        new List<string>{"#","Producto","Precio unitario","Cantidad","Total"},
        new List<string>{"1","Gansito","25","2","50"},
        new List<string>{"2","Polvorones","100","1","100"},
        new List<string>{"3","Pollo asado","200","2","400"}
    };

var document = new Report(
    new ReportData
    {
        Title ="titulo",
        IssueDate =new DateOnly(2026,06,17),
        DateStart=new DateOnly(2023,11,28),
        DateEnd=new DateOnly(2025,06,17),
        Comments="Comentarios declarado en nuestra clase",
        ColumnsAndRows = listaEjemplo,
        HasTotal =true,
        TotalColumn=4
    }
);

// instead of the standard way of generating a PDF file
document.GeneratePdf("hello.pdf");

// use the following invocation
document.ShowInCompanion();

// optionally, you can specify an HTTP port to communicate with the previewer host (default is 12500)
document.ShowInCompanion(12500);