using System.Reflection;
using PDFGenerator.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class Report : IDocument
{
    public ReportData Model { get; }
    public Report(ReportData model)
    {
        this.Model=model;
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
                    .Text($"Invoice #{Model.Title}")
                    .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                column.Item().Text(text =>
                {
                    text.Span("Issue date: ").SemiBold();
                    text.Span($"{Model.IssueDate:d}");
                });
                
                column.Item().Text(text =>
                {
                    text.Span("Due date: ").SemiBold();
                    text.Span($"{Model.DateEnd:d}");
                });
            });

            row.ConstantItem(100).Height(50).Placeholder();
        });
    }

    void ComposeContent(IContainer container)
    {
        container
            .PaddingVertical(40)
            .Height(250)
            .Background(Colors.Grey.Lighten3)
            .AlignCenter()
            .AlignMiddle()
            .Text("Content").FontSize(16);
    }
}