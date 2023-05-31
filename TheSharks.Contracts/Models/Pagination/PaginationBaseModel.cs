namespace TheSharks.Contracts.Models.Pagination;

public class PaginationBaseModel
{
    public int Page { get; set; } = 1;
    public int RecordsPerPage { get; set; } = 10;
}