using MediatR;
using TheSharks.Contracts.Models.Statistics;

namespace TheSharks.Application.Handlers.Statistics.Queries.GetAllStatistics;

public class GetAllStatisticsQuery : IRequest<StatisticsOverviewListModel>
{
    public DateTimeOffset? DateFilter { get; set; }
    public DateTimeOffset? MonthFilter { get; set; }
    public DateTimeOffset? YearFilter { get; set; }
    public string? PageFilter { get; set; }
}