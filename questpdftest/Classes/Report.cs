using System.Reflection;
using PDFGenerator.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class Report : IDocument
{
    public ReportData _reportData { get; }
    public Report(ReportData reportData)
    {
        _reportData=reportData;
    }

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);
            
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                

                page.Footer().AlignCenter().Text(x =>
                {
                    x.CurrentPageNumber();
                    x.Span(" / ");
                    x.TotalPages();
                });
            });
    }

    void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column( column =>
            {
                column.Item()
                    .Text($"Invoice {_reportData.Title}")
                    .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                column.Item().Text(text =>
                {
                    text.Span("Issue date: ").SemiBold();
                    text.Span($"{_reportData.DateStart:d}");
                });
                
                column.Item().Text(text =>
                {
                    text.Span("Due date: ").SemiBold();
                    text.Span($"{_reportData.DateEnd:d}");
                });
            });

            row.ConstantItem(100).Height(50).Placeholder();
        });
    }

    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(40).Column(column=>
        {
            column.Spacing(5);
            column.Item().Element(ComposeTable);

            

            if (!string.IsNullOrWhiteSpace(_reportData.Comments))
                    column.Item().PaddingTop(25).Element(ComposeComments);
        });
    }

     void ComposeTable(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                foreach (string item in _reportData.ColumnsAndRows.ElementAt(0))
                {
                    System.Console.WriteLine($"Headers columnas {item}");
                    columns.RelativeColumn();
                }
 
            });
            
            table.Header(header =>
            {
                foreach (string item in _reportData.ColumnsAndRows.ElementAt(0))
                {
                    System.Console.WriteLine($"Headers columnas {item}");
                    header.Cell().Element(CellStyle).Text(item);
                }
                
                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });
            
            for (int i = 1; i < _reportData.ColumnsAndRows.Count; i++)
            {

                foreach (string item in _reportData.ColumnsAndRows.ElementAt(i))
                {
                    System.Console.WriteLine(item);
                    table.Cell().Element(CellStyle).Text(item);
                }

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            }
        });
    }

     void ComposeComments(IContainer container)
    {
        container.Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
        {
            column.Spacing(5);
            column.Item().Text("Comments").FontSize(14);
            column.Item().Text(_reportData.Comments);
        });
    }
}