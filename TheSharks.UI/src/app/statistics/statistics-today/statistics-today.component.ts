import { SelectionModel } from '@angular/cdk/collections';
import { Breakpoints, BreakpointObserver } from '@angular/cdk/layout';
import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute } from '@angular/router';
import { map, Observable, shareReplay } from 'rxjs';
import { AuthService } from 'src/app/services/auth/auth.service';
import { StatisticsService } from 'src/app/services/statistics/statistics.service';
import { StatisticsOverviewListDto } from 'src/types/dto/statistics/StatisticsOverviewListDto';
import { Claims } from 'src/types/application/role/Claims';

interface StatisticsItem {
    icon: string,
    description: string,
    statistic: string
}

@Component({
    selector: 'app-statistics-today',
    templateUrl: './statistics-today.component.html',
    styleUrls: ['./statistics-today.component.scss']
})

export class StatisticsTodayComponent implements OnInit {

    dataSource: MatTableDataSource<StatisticsItem> | undefined
    columns = ["icon", "description", "statistics"]
    authorized = false
    statisticsDescription: string | undefined

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

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
            this.statisticsDescription = "STATISTIEKEN VANDAAG - " + response.today.description

            let stat1: StatisticsItem = {
                icon: "receipt",
                description: "Totaal aantal bezoeken",
                statistic: response.today.totalVisits.toString()
            }
            statistics.push(stat1);
            
            let stat2: StatisticsItem = {
                icon: "assignment_ind",
                description: "Totaal aantal ingelogde bezoeken",
                statistic: response.today.totalAuthenticatedVisits.toString()
            }
            statistics.push(stat2);
            
            let stat3: StatisticsItem = {
                icon: "mobile_friendly",
                description: "Totaal aantal bezoeken via de app",
                statistic: response.today.totalAppVisits.toString()
            }
            statistics.push(stat3);
            
            let stat4: StatisticsItem = {
                icon: "receipt",
                description: "Totaal aantal unieke bezoekers",
                statistic: response.today.totalUniqueVisits.toString()
            }
            statistics.push(stat4);
            
            let stat5: StatisticsItem = {
                icon: "assignment_ind",
                description: "Totaal aantal unieke ingelogde bezoekers",
                statistic: response.today.totalUniqueAuthenticatedVisits.toString()
            }
            statistics.push(stat5);

            this.dataSource = new MatTableDataSource(statistics);
        }
    }

    private async checkClaims() {
        this.authorized = await this._as.authorizeClaims([Claims.ManageStatistics])
    }
}