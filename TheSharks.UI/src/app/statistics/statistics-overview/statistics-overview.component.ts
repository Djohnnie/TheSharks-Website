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
import { StatisticsDetailsDto } from 'src/types/dto/statistics/StatisticsDetailsDto';
import { Claims } from 'src/types/application/role/Claims';

interface StatisticsItem {
    icon: string,
    description: string,
    statistic: string
}

@Component({
    selector: 'app-statistics-overview',
    templateUrl: './statistics-overview.component.html',
    styleUrls: ['./statistics-overview.component.scss']
})

export class StatisticsOverviewComponent implements OnInit {

    dataSource: MatTableDataSource<StatisticsItem> | undefined
    detailDataSource: MatTableDataSource<StatisticsDetailsDto> | undefined
    columns = ["icon", "description", "statistics"]
    detailColumns = ["date", "page", "isLoggedIn", "isApp"]
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
            this.statisticsDescription = "LEVENSLANGE STATISTIEKEN"

            let stat1: StatisticsItem = {
                icon: "receipt",
                description: "Totaal aantal bezoeken",
                statistic: response.totalVisits.toString()
            }
            statistics.push(stat1);
            
            let stat2: StatisticsItem = {
                icon: "assignment_ind",
                description: "Totaal aantal ingelogde bezoeken",
                statistic: response.totalAuthenticatedVisits.toString()
            }
            statistics.push(stat2);
            
            let stat3: StatisticsItem = {
                icon: "mobile_friendly",
                description: "Totaal aantal bezoeken via de app",
                statistic: response.totalAppVisits.toString()
            }
            statistics.push(stat3);
            
            let stat4: StatisticsItem = {
                icon: "receipt",
                description: "Totaal aantal unieke bezoekers",
                statistic: response.totalUniqueVisits.toString()
            }
            statistics.push(stat4);
            
            let stat5: StatisticsItem = {
                icon: "assignment_ind",
                description: "Totaal aantal unieke ingelogde bezoekers",
                statistic: response.totalUniqueAuthenticatedVisits.toString()
            }
            statistics.push(stat5);
            
            let stat6: StatisticsItem = {
                icon: "class",
                description: "Meest bezochte pagina",
                statistic: response.topPage
            }
            statistics.push(stat6);
            
            let stat7: StatisticsItem = {
                icon: "class",
                description: "Meest bezochte ingelogde pagina",
                statistic: response.topAuthenticatedPage
            }
            statistics.push(stat7);

            this.dataSource = new MatTableDataSource(statistics);
            this.detailDataSource = new MatTableDataSource(response.mostRecent);
        }        
    }

    private async checkClaims() {
        this.authorized = await this._as.authorizeClaims([Claims.ManageStatistics])
    }
}