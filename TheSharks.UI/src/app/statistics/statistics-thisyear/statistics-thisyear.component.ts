import { SelectionModel } from '@angular/cdk/collections';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnInit, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { map, Observable, shareReplay } from 'rxjs';
import { AuthService } from 'src/app/services/auth/auth.service';
import { StatisticsService } from 'src/app/services/statistics/statistics.service';
import { StatisticsOverviewListDto } from 'src/types/dto/statistics/StatisticsOverviewListDto';
import { Claims } from 'src/types/application/role/Claims';
import { ChartConfiguration, ChartData, ChartEvent, ChartType } from 'chart.js';
import { BaseChartDirective } from 'ng2-charts';

interface StatisticsItem {
    icon: string,
    description: string,
    statistic: string
}

@Component({
    selector: 'app-statistics-thisyear',
    templateUrl: './statistics-thisyear.component.html',
    styleUrls: ['./statistics-thisyear.component.scss']
})

export class StatisticsThisYearComponent implements OnInit {

    @ViewChild(BaseChartDirective) chart: BaseChartDirective | undefined;

    dataSource: MatTableDataSource<StatisticsItem> | undefined
    columns = ["icon", "description", "statistics"]
    authorized = false
    statisticsDescription: string | undefined

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    public barChartOptions: ChartConfiguration['options'] = {
        responsive: true,
        scales: {
            x: {},
            y: {}
        },
        plugins: {
            legend: {
            display: true,
            },
        }
    };      
    public barChartType: ChartType = 'bar';    
    public barChartData: ChartData<'bar'> = {
        labels: [],
        datasets: []
    };

    constructor(
        private _ss: StatisticsService,
        private _as: AuthService,
        public dialog: MatDialog,
        private _bo: BreakpointObserver
    ) { }

    async ngOnInit() {        
        await this.checkClaims()

        const response = (await this._ss.getAllStatistics()).response;

        if( response )
        {
            const statistics = new Array<StatisticsItem>();
            this.statisticsDescription = "STATISTIEKEN DIT JAAR - " + response.thisYear.description

            let stat1: StatisticsItem = {
                icon: "receipt",
                description: "Totaal aantal bezoeken",
                statistic: response.thisYear.totalVisits.toString()
            }
            statistics.push(stat1);
            
            let stat2: StatisticsItem = {
                icon: "assignment_ind",
                description: "Totaal aantal ingelogde bezoeken",
                statistic: response.thisYear.totalAuthenticatedVisits.toString()
            }
            statistics.push(stat2);
            
            let stat3: StatisticsItem = {
                icon: "mobile_friendly",
                description: "Totaal aantal bezoeken via de app",
                statistic: response.thisYear.totalAppVisits.toString()
            }
            statistics.push(stat3);
            
            let stat4: StatisticsItem = {
                icon: "receipt",
                description: "Totaal aantal unieke bezoekers",
                statistic: response.thisYear.totalUniqueVisits.toString()
            }
            statistics.push(stat4);
            
            let stat5: StatisticsItem = {
                icon: "assignment_ind",
                description: "Totaal aantal unieke ingelogde bezoekers",
                statistic: response.thisYear.totalUniqueAuthenticatedVisits.toString()
            }
            statistics.push(stat5);

            this.dataSource = new MatTableDataSource(statistics);

            this.barChartData.labels = response.thisYear.subPeriods.map( x => x.description );
            this.barChartData.datasets.push( {
                data: response.thisYear.subPeriods.map( x => x.totalVisits ),
                label: "Totaal aantal bezoeken"
            });
            this.barChartData.datasets.push( {
                data: response.thisYear.subPeriods.map( x => x.totalAuthenticatedVisits ),
                label: "Totaal aantal ingelogde bezoeken"
            });
            this.barChartData.datasets.push( {
                data: response.thisYear.subPeriods.map( x => x.totalAppVisits ),
                label: "Totaal aantal bezoeken via de app"
            });
        }
    }

    private async checkClaims() {
        this.authorized = await this._as.authorizeClaims([Claims.ManageStatistics])
    }
}