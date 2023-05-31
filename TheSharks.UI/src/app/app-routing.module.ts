import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Claims } from 'src/types/application/role/Claims';
import { ActivityDetailsComponent } from './activity/activity-details/activity-details.component';
import { ActivityListComponent } from './activity/activity-list/activity-list.component';
import { ActivityLightComponent } from './activity/activity-light/activity-light.component';
import { AddActivityComponent } from './activity/add-activity/add-activity.component';
import { EditActivityComponent } from './activity/edit-activity/edit-activity.component';
import { EnrollInActivityComponent } from './activity/enroll-in-activity/enroll-in-activity.component';
import { CmsMenuComponent } from './cms/cms-menu/cms-menu.component';
import { EditPageComponent } from './cms/edit-page/edit-page.component';
import { ManageMenuTreeComponent } from './cms/manage-menu-tree/manage-menu-tree.component';
import { ManagePagesComponent } from './cms/manage-pages/manage-pages.component';
import { PageComponent } from './cms/page/page.component';
import { NotFoundComponent } from './error/not-found/not-found.component';
import { AnonGuard } from './guards/anon.guard';
import { AuthGuard } from './guards/auth.guard';
import { ClaimGuard } from './guards/claim.guard';
import { DefaultGuard } from './guards/default.guard';
import { GroupListComponent } from './member/group-list/group-list.component';
import { MemberDetailsComponent } from './member/member-details/member-details.component';
import { MemberListComponent } from './member/member-list/member-list.component';
import { MemberMenuComponent } from './member/member-menu/member-menu.component';
import { AddNewsItemComponent } from './news/add-news-item/add-news-item.component';
import { EditNewsItemComponent } from './news/edit-news-item/edit-news-item.component';
import { NewsItemListComponent } from './news/news-item-list/news-item-list.component';
import { AddRoleComponent } from './role/add-role/add-role.component';
import { EditRoleComponent } from './role/edit-role/edit-role.component';
import { ManageRolesComponent } from './role/manage-roles/manage-roles.component';
import { EditProfileComponent } from './user/edit-profile/edit-profile.component';
import { EditMemberComponent } from "./member/edit-member/edit-member.component";
import { ForgotPasswordComponent } from './user/forgot-password/forgot-password.component';
import { LoginComponent } from './user/login/login.component';
import { RegisterComponent } from './user/register/register.component';
import { ResetPasswordComponent } from './user/reset-password/reset-password.component';
import { AddGalleryComponent } from './gallery/add-gallery/add-gallery.component';
import { GalleryListComponent } from './gallery/gallery-list/gallery-list.component';
import { GalleryDetailsComponent } from './gallery/gallery-details/gallery-details.component';
import { RemoveGalleryComponent } from './gallery/remove-gallery-dialog/remove-gallery.component';
import { DocumentListComponent } from './document/document-list/document-list.component';
import { StatisticsMenuComponent } from './statistics/statistics-menu/statistics-menu.component';
import { StatisticsOverviewComponent } from './statistics/statistics-overview/statistics-overview.component';
import { StatisticsTodayComponent } from "./statistics/statistics-today/statistics-today.component";
import { StatisticsThisWeekComponent } from "./statistics/statistics-thisweek/statistics-thisweek.component";
import { StatisticsThisMonthComponent } from "./statistics/statistics-thismonth/statistics-thismonth.component";
import { StatisticsThisYearComponent } from "./statistics/statistics-thisyear/statistics-thisyear.component";
import { PrivacyComponent } from "./privacy/privacy.component";
import { DownloadAppComponent } from "./download-app/download-app.component";
import { HelpComponent } from "./help/help.component";
import { OpenWaterTestListComponent } from "./open-water-test/open-water-test-list/open-water-test-list.component";
import { AddOpenWaterTestComponent } from "./open-water-test/add-open-water-test/add-open-water-test.component";
import { EditOpenWaterTestComponent } from "./open-water-test/edit-open-water-test/edit-open-water-test.component";

const routes: Routes = [
    {
        path: "login",
        canActivate: [AnonGuard],
        component: LoginComponent,
        data: { title: "Aanmelden" }
    },
    {
        path: "profile",
        canActivate: [AuthGuard],
        component: EditProfileComponent,
        data: { title: "Profiel" }
    },
    {
        path: "editmember/:id",
        canActivate: [AuthGuard],
        component: EditMemberComponent,
        data: { title: "Lid aanpassen" }
    },
    {
        path: "register",
        component: RegisterComponent,
        canActivate: [AuthGuard, ClaimGuard],
        data: { title: "Registreren", claims: [Claims.ManageMembers] }
    },
    {
        path: "forgot-password",
        canActivate: [AnonGuard],
        component: ForgotPasswordComponent
    },
    {
        path: "reset-password",
        canActivate: [AnonGuard],
        component: ResetPasswordComponent
    },
    {
        path: "activities",
        canActivate: [AuthGuard],
        component: ActivityListComponent,
        data: { title: "Activiteiten" }
    },
    {
        path: "activities-light",
        canActivate: [AnonGuard],
        component: ActivityLightComponent,
        data: { title: "Activiteiten" }
    },
    {
        path: "activities/:type/:id",
        canActivate: [AuthGuard],
        component: ActivityDetailsComponent,
        data: { title: "Activiteit" }
    },
    {
        path: "add-activity",
        canActivate: [AuthGuard, ClaimGuard],
        component: AddActivityComponent,
        data: { title: "Activiteit aanmaken", claims: [Claims.ManageActivities] }
    },
    {
        path: "edit-activity/:type/:id",
        canActivate: [AuthGuard, ClaimGuard],
        component: EditActivityComponent,
        data: { title: "Activiteit bewerken", claims: [Claims.ManageActivities] }
    },
    {
        path: "enroll/:type/:id",
        canActivate: [AuthGuard],
        component: EnrollInActivityComponent,
        data: { title: "Inschrijven" }
    },
    {
        path: "news-items",
        component: NewsItemListComponent,
        data: { title: "Nieuws" }
    },
    {
        path: "add-news-item",
        canActivate: [AuthGuard, ClaimGuard],
        component: AddNewsItemComponent,
        data: { title: "Post aanmaken", claims: [Claims.ManageNewsItems] }
    },
    {
        path: "edit-news-item/:id",
        canActivate: [AuthGuard, ClaimGuard],
        component: EditNewsItemComponent,
        data: { title: "Bewerk post", claims: [Claims.ManageNewsItems] }
    },
    {
        path: "add-role",
        canActivate: [AuthGuard, ClaimGuard],
        component: AddRoleComponent,
        data: { title: "Rol toevoegen", claims: [Claims.ManageMembers] }
    },
    {
        path: "roles",
        canActivate: [AuthGuard, ClaimGuard],
        component: ManageRolesComponent,
        data: { title: "Rollen beheren", claims: [Claims.ManageMembers] }
    },
    {
        path: "roles/:id",
        canActivate: [AuthGuard, ClaimGuard],
        component: EditRoleComponent,
        data: { title: "Rol bewerken", claims: [Claims.ManageMembers] }
    },
    {
        path: "open-water-tests",
        canActivate: [AuthGuard, ClaimGuard],
        component: OpenWaterTestListComponent,
        data: { title: "Openwaterproeven beheren", claims: [Claims.ManageActivities] }
    },
    {
        path: "open-water-tests/add",
        canActivate: [AuthGuard, ClaimGuard],
        component: AddOpenWaterTestComponent,
        data: { title: "Openwaterproef toevoegen", claims: [Claims.ManageActivities] }
    },
    {
        path: "open-water-tests/edit/:id",
        canActivate: [AuthGuard, ClaimGuard],
        component: EditOpenWaterTestComponent,
        data: { title: "Openwaterproef aanpassen", claims: [Claims.ManageActivities] }
    },
    {
        path: "members",
        canActivate: [AuthGuard],
        component: MemberMenuComponent,
        data: { title: "Leden menu" },
        children: [
            {
                path: "",
                redirectTo: "memberlist",
                pathMatch: "full"
            },
            {
                path: "memberlist",
                component: MemberListComponent,
                data: { title: "Leden" },
            },
            {
                path: "grouplist",
                component: GroupListComponent,
                data: { title: "Groepen" },
            }
        ]
    },
    {
        path: "members/:id",
        canActivate: [AuthGuard],
        component: MemberDetailsComponent,
        data: { title: "Lid" }
    },
    {
        path: "page/:link",
        component: PageComponent
    },
    {
        path: "editPage/:link",
        canActivate: [AuthGuard, ClaimGuard],
        component: EditPageComponent,
        data: { title: "Pagina bewerken", claims: [Claims.ManagePageContent] }
    },
    {
        path: "contentmanager",
        canActivate: [AuthGuard, ClaimGuard],
        component: CmsMenuComponent,
        data: { title: "Contentmanager", claims: [Claims.ManagePageContent] },
        children: [
            {
                path: "",
                redirectTo: "pages",
                pathMatch: "full"
            },
            {
                path: "menu",
                component: ManageMenuTreeComponent,
                data: { title: "CMS - Menu" }
            },
            {
                path: "pages",
                component: ManagePagesComponent,
                data: { title: "CMS - Pagina's" }
            }
        ]
    },
    {
        path: "statistics",
        canActivate: [AuthGuard, ClaimGuard],
        component: StatisticsMenuComponent,
        data: { title: "Statistieken", claims: [Claims.ManageStatistics] },
        children: [
            {
                path: "",
                redirectTo: "overview",
                pathMatch: "full"
            },
            {
                path: "overview",
                component: StatisticsOverviewComponent,
                data: { title: "Statistieken - Overzicht" },
            },
            {
                path: "today",
                component: StatisticsTodayComponent,
                data: { title: "Statistieken - Vandaag" },
            },
            {
                path: "week",
                component: StatisticsThisWeekComponent,
                data: { title: "Statistieken - Deze week" },
            },
            {
                path: "month",
                component: StatisticsThisMonthComponent,
                data: { title: "Statistieken - Deze maand" },
            },
            {
                path: "year",
                component: StatisticsThisYearComponent,
                data: { title: "Statistieken - Dit jaar" },
            }
        ]
    },
    {
        path: "galleries",
        canActivate: [AuthGuard],
        component: GalleryListComponent,
        data: { title: "Gallerijen" }
    },
    {
        path: "galleries/:id",
        canActivate: [AuthGuard],
        component: GalleryDetailsComponent,
        data: { title: "Gallerij" }
    },
    {
        path: "add-gallery",
        canActivate: [AuthGuard, ClaimGuard],
        component: AddGalleryComponent,
        data: { title: "Gallerij aanmaken", claims: [Claims.ManageGalleries] }
    },
    {
        path: "remove-gallery/:id",
        canActivate: [AuthGuard, ClaimGuard],
        component: RemoveGalleryComponent,
        data: { title: "Gallerij verwijderen", claims: [Claims.ManageGalleries] }
    },
    {
        path: "documents",
        canActivate: [AuthGuard],
        component: DocumentListComponent,
        data: { title: "Documenten" }
    },
    {
        path: "privacy",
        component: PrivacyComponent,
        data: { title: "Privacy" }
    },
    {
        path: "app",
        component: DownloadAppComponent,
        data: { title: "Download App" }
    },
    {
        path: "help",
        component: HelpComponent,
        data: { title: "Hulp krijgen" }
    },
    {
        path: "",
        canActivate: [DefaultGuard],
        pathMatch: "full"
    },
    {
        path: "**",
        component: NotFoundComponent,
        data: { title: "404" }
    }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }