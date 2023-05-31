namespace TheSharks.Contracts.Models.NewsItems;

public class GetNewsItemModel
{
    public Guid AuthorId { get; set; }
    public Guid Id { get; set; }
    public string AuthorFirstName { get; set; }
    public string AuthorLastName { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTimeOffset PublishDate { get; set; }
    public bool MembersOnly { get; set; }
}

public class GetNewsItemsModel
{
    public int TotalRecords { get; set; }
    public IEnumerable<GetNewsItemModel> NewsItems { get; set; }
}