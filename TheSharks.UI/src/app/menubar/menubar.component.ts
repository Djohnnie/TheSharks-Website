import { Component, OnDestroy, OnInit } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable, Subscription } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { LogoutDialogComponent } from '../user/logout-dialog/logout-dialog.component';
import { AuthService } from '../services/auth/auth.service';
import { NestedTreeControl } from '@angular/cdk/tree';
import { MatTree, MatTreeNestedDataSource, MatTreeFlattener, MatTreeNode } from '@angular/material/tree';
import { LinkNode } from 'src/types/application/cms/LinkNode';
import { CmsService } from '../services/cms/cms.service';
import { MatSnackBar } from '@angular/material/snack-bar';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { Claims } from 'src/types/application/role/Claims';
import { UserService } from '../services/user/user.service';
import { StatusService } from '../services/status/status.service';
import { User } from 'src/types/application/user/User';

interface FlatNode {
    expandable: boolean;
    title: string;
    link: string;
    level: number;
    isMembersOnly: boolean;
}

@Component({
    selector: 'app-menubar',
    templateUrl: './menubar.component.html',
    styleUrls: ['./menubar.component.scss'],
})
export class MenubarComponent implements OnInit, OnDestroy {
    loggedIn$: Observable<boolean>;
    authCms$: Observable<boolean>;
    authRoles$: Observable<boolean>;
    authStatistics$: Observable<boolean>;
    authActivities$: Observable<boolean>;
    currentUser$: Observable<User | undefined>

    greeting = ""
    title = "The Sharks"
    version: any | undefined

    private _subContent: Subscription | undefined
    private _subUser: Subscription | undefined

    treeControl = new NestedTreeControl<LinkNode>(node => node.children ?? new Array<LinkNode>());

    dataSource = new MatTreeNestedDataSource<LinkNode>();

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    constructor(
        private _bo: BreakpointObserver,
        private _dialog: MatDialog,
        private _as: AuthService,
        private _us: UserService,
        private _ss: StatusService,
        private _cmss: CmsService,
        private _sb: MatSnackBar
    ) {
        this.loggedIn$ = this._as.loggedIn$
        this.currentUser$ = this._us.currentUser$
        this.authCms$ = this._as.authorizeClaims$([Claims.ManagePageContent])
        this.authRoles$ = this._as.authorizeClaims$([Claims.ManageMembers])
        this.authStatistics$ = this._as.authorizeClaims$([Claims.ManageStatistics])
        this.authActivities$ = this._as.authorizeClaims$([Claims.ManageActivities])
    }

    ngOnDestroy(): void {
        if (this._subContent) this._subContent.unsubscribe()
        if (this._subUser) this._subUser.unsubscribe()
    }

    async ngOnInit() {
        this.version = (await this._ss.getVersion()).response
        await this.loadMenu()
        this._subContent = this._cmss.contentChanges$().subscribe(() => this.loadMenu())
        this._subUser = this.currentUser$.subscribe(() => this.loadGreeting())
    }

    public closeDrawer(drawer: any) {
        if( drawer.mode === 'over' )
        {
            drawer.close();
        }
    }

    private async loadGreeting() {
        const now = (new Date).toLocaleTimeString("nl-BE")
        const [hour, minute, second] = now.split(":").map(v => +v)
        const user = await this._us.getUser()

        if (!user.response) return
        const name = user.response.firstName

        if (hour >= 0 && hour < 5) this.greeting = "Goeienacht, "
        else if (hour >= 5 && hour < 10) this.greeting = "Goeiemorgen, "
        else if (hour >= 10 && hour < 18) this.greeting = "Goedemiddag, "
        else if (hour >= 18) this.greeting = "Goedenavond, "

        this.greeting = this.greeting + name
    }

    private async loadMenu() {
        const response = await this._cmss.getMenuTree()

        if (response.error === undefined) {
            this.dataSource.data = response.response!
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }
    }

    onDialogOpen() {
        this._dialog.open(LogoutDialogComponent)
    }

    hasChild = (_: number, node: LinkNode) => !!node.children && node.children.length > 0;

    openLink(url: string) {
        window.open(url)
    }
}