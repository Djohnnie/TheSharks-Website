import { StatisticsPeriodDto } from "./StatisticsPeriodDto";
import { StatisticsDetailsDto } from "./StatisticsDetailsDto";

export interface StatisticsOverviewListDto {
    totalVisits: number;
    totalAuthenticatedVisits: number;
    totalAppVisits: number;
    totalUniqueVisits: number;
    totalUniqueAuthenticatedVisits: number;
    topPage: string;
    topAuthenticatedPage: string;
    today: StatisticsPeriodDto;
    thisWeek: StatisticsPeriodDto;
    thisMonth: StatisticsPeriodDto;
    thisYear: StatisticsPeriodDto;
    mostRecent: StatisticsDetailsDto[];
}