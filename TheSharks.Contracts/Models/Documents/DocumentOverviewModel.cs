using TheSharks.Contracts.Models.Common;

namespace TheSharks.Contracts.Models.Documents;

public class DocumentOverviewModel
{
    public int TotalRecords { get; set; }
    public IEnumerable<DocumentModel> Documents { get; set; }
}

public class DocumentModel : BaseIdModel
{
    public string Name { get; set; }
    public string FileName { get; set; }
    public bool IsImportant { get; set; }
}