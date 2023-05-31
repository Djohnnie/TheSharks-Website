using TheSharks.Contracts.Models.Common;

namespace TheSharks.Contracts.Models.Galleries;

public class GetGalleryModel : BaseIdModel
{
    public string Name { get; set; }
    public IEnumerable<GetGalleryPictureModel> Pictures { get; set; }
}

public class GetGalleryPictureModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string StorageUrl { get; set; }
}