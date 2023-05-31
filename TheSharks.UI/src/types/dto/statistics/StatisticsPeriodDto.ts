export interface StatisticsPeriodDto {
    description: string;
    totalVisits: number;
    totalAuthenticatedVisits: number;   
    totalAppVisits: number; 
    totalUniqueVisits: number;
    totalUniqueAuthenticatedVisits: number;
    subPeriods: StatisticsPeriodDto[];
}