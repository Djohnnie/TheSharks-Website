using TheSharks.Contracts.Models.Common;

namespace TheSharks.Contracts.Models.Galleries;

public class GalleryOverviewListModel
{
    public IEnumerable<GalleryOverviewModel> Galleries { get; set; }
    public int TotalRecords { get; set; }
}

public class GalleryOverviewModel : BaseIdModel
{
    public string Name { get; set; }
    public int AmountPictures { get; set; }
    public string? UrlFirstPicture { get; set; }
}