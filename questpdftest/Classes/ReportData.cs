using System.Reflection.Metadata.Ecma335;

namespace PDFGenerator.Models;

public class ReportData
{
    public required string Title { get; set; }
    public DateOnly IssueDate { get; set; }
    public DateOnly DateStart { get; set; }
    public DateOnly DateEnd { get; set; }
    public string? Comments { get; set; }
    public required List<List<string>> ColumnsAndRows { get; set; }
    public bool HasTotal { get; set; }
    public int?  TotalColumn { get; set; }

    public bool VerifyTotalColumn()
    {
        
        if (!this.HasTotal || this.TotalColumn is null)
                return false;

            int colIndex = this.TotalColumn.Value;
        
            string value = ColumnsAndRows
                .ElementAt(0)
                .ElementAt(colIndex);
        //verify if it can be summed up as double for display of a total value
        return true;

    }
}
