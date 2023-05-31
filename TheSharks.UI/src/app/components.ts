import { ActivityDetailsOnlyComponent } from "./activity/activity-details-only/activity-details-only.component";
import { ActivityDetailsComponent } from "./activity/activity-details/activity-details.component";
import { ActivityListComponent } from "./activity/activity-list/activity-list.component";
import { ActivityLightComponent } from "./activity/activity-light/activity-light.component";
import { AddActivityComponent } from "./activity/add-activity/add-activity.component";
import { EnrollInActivityComponent } from "./activity/enroll-in-activity/enroll-in-activity.component";
import { MemberListDialogComponent } from "./activity/member-list-dialog/member-list-dialog.component";
import { ParticipantDialogComponent } from "./activity/participant-dialog/participant-dialog.component";
import { AppComponent } from "./app.component";
import { AddPageDialogComponent } from "./cms/add-page-dialog/add-page-dialog.component";
import { CmsMenuComponent } from "./cms/cms-menu/cms-menu.component";
import { EditPageComponent } from "./cms/edit-page/edit-page.component";
import { ManageMenuTreeComponent } from "./cms/manage-menu-tree/manage-menu-tree.component";
import { ManagePagesComponent } from "./cms/manage-pages/manage-pages.component";
import { PageComponent } from "./cms/page/page.component";
import { NotFoundComponent } from "./error/not-found/not-found.component";
import { HomeComponent } from "./home/home.component";
import { MemberDetailsComponent } from "./member/member-details/member-details.component";
import { MemberListComponent } from "./member/member-list/member-list.component";
import { MenubarComponent } from "./menubar/menubar.component";
import { AddNewsItemComponent } from "./news/add-news-item/add-news-item.component";
import { EditNewsItemComponent } from "./news/edit-news-item/edit-news-item.component";
import { NewsItemListComponent } from "./news/news-item-list/news-item-list.component";
import { SkipSanitizePipe } from "./pipes/skip-sanitize.pipe";
import { AddRoleComponent } from "./role/add-role/add-role.component";
import { EditRoleComponent } from "./role/edit-role/edit-role.component";
import { EditProfileComponent } from "./user/edit-profile/edit-profile.component";
import { EditMemberComponent } from "./member/edit-member/edit-member.component";
import { DeleteMemberDialogComponent } from "./member/delete-member-dialog/delete-member-dialog.component";
import { RemoveMyselfDialogComponent } from "./user/remove-myself-dialog/remove-myself-dialog.component";
import { ForgotPasswordComponent } from "./user/forgot-password/forgot-password.component";
import { LoginComponent } from "./user/login/login.component";
import { LogoutDialogComponent } from "./user/logout-dialog/logout-dialog.component";
import { RegisterComponent } from "./user/register/register.component";
import { ResetPasswordComponent } from "./user/reset-password/reset-password.component";
import { AddGalleryComponent } from "./gallery/add-gallery/add-gallery.component";
import { GalleryListComponent } from "./gallery/gallery-list/gallery-list.component";
import { GalleryDetailsComponent } from "./gallery/gallery-details/gallery-details.component";
import { DropDirective, UploadPictureComponent } from "./gallery/upload-picture-dialog/upload-picture.component";
import { ActionMenuComponent } from "./action-menu/action-menu.component";
import { EditActivityComponent } from "./activity/edit-activity/edit-activity.component";
import { DeleteActivityDialogComponent } from "./activity/delete-activity-dialog/delete-activity-dialog.component";
import { ActionMenuButtonComponent } from "./action-menu/action-menu-button/action-menu-button.component";
import { ComponentDialogComponent } from "./cms/component-dialog/component-dialog.component";
import { DisplayComponent } from "./cms/components/display/display.component";
import { LastNewsItemComponent } from "./cms/components/last-news-item/last-news-item.component";
import { UpcomingActivityComponent } from "./cms/components/upcoming-activity/upcoming-activity.component";
import { EmailDialogComponent } from "./member/email-dialog/email-dialog.component";
import { GroupListComponent } from "./member/group-list/group-list.component";
import { MemberMenuComponent } from "./member/member-menu/member-menu.component";
import { PaginatorComponent } from "./paginator/paginator.component";
import { RemoveGalleryComponent } from "./gallery/remove-gallery-dialog/remove-gallery.component";
import { AddDocumentDialogComponent } from "./document/add-document-dialog/add-document-dialog.component";
import { DeleteDocumentDialogComponent } from "./document/delete-document-dialog/delete-document-dialog.component";
import { DocumentListComponent } from "./document/document-list/document-list.component";
import { DeleteNewsItemDialogComponent } from "./news/delete-news-item-dialog/delete-news-item-dialog.component";
import { ManageRolesComponent } from "./role/manage-roles/manage-roles.component";
import { StatisticsMenuComponent } from "./statistics/statistics-menu/statistics-menu.component";
import { StatisticsOverviewComponent } from "./statistics/statistics-overview/statistics-overview.component";
import { StatisticsTodayComponent } from "./statistics/statistics-today/statistics-today.component";
import { StatisticsThisWeekComponent } from "./statistics/statistics-thisweek/statistics-thisweek.component";
import { StatisticsThisMonthComponent } from "./statistics/statistics-thismonth/statistics-thismonth.component";
import { StatisticsThisYearComponent } from "./statistics/statistics-thisyear/statistics-thisyear.component";
import { PrivacyComponent } from "./privacy/privacy.component";
import { DownloadAppComponent } from "./download-app/download-app.component";
import { HelpComponent } from "./help/help.component";
import { OpenWaterTestListComponent } from "./open-water-test/open-water-test-list/open-water-test-list.component";
import { OpenWaterTestContentDialogComponent } from "./open-water-test/open-water-test-content-dialog/open-water-test-content-dialog.component";
import { AddOpenWaterTestComponent } from "./open-water-test/add-open-water-test/add-open-water-test.component";
import { EditOpenWaterTestComponent } from "./open-water-test/edit-open-water-test/edit-open-water-test.component";
import { DeleteOpenWaterTestDialogComponent } from "./open-water-test/delete-open-water-test-dialog/delete-open-water-test-dialog.component";

export const AppComponents = [
    AppComponent,
    MenubarComponent,
    LoginComponent,
    HomeComponent,
    EditProfileComponent,
    EditMemberComponent,
    LogoutDialogComponent,
    RegisterComponent,
    ActivityListComponent,
    ActivityLightComponent,
    ActivityDetailsComponent,
    ForgotPasswordComponent,
    ParticipantDialogComponent,
    ResetPasswordComponent,
    ResetPasswordComponent,
    AddActivityComponent,
    EnrollInActivityComponent,
    ActivityDetailsOnlyComponent,
    AddNewsItemComponent,
    MemberListDialogComponent,
    DeleteMemberDialogComponent,
    RemoveMyselfDialogComponent,
    EditNewsItemComponent,
    AddRoleComponent,
    EditNewsItemComponent,
    SkipSanitizePipe,
    EditRoleComponent,
    MemberListComponent,
    MemberDetailsComponent,
    PageComponent,
    ManageMenuTreeComponent,
    CmsMenuComponent,
    ManagePagesComponent,
    AddPageDialogComponent,
    EditPageComponent,
    NotFoundComponent,
    AddGalleryComponent,
    GalleryDetailsComponent,
    RemoveGalleryComponent,
    UploadPictureComponent,
    ActionMenuComponent,
    EmailDialogComponent,
    MemberMenuComponent,
    GroupListComponent,
    EditActivityComponent,
    DeleteActivityDialogComponent,
    PaginatorComponent,
    ActionMenuButtonComponent,
    DropDirective,
    UpcomingActivityComponent,
    ComponentDialogComponent,
    LastNewsItemComponent,
    DisplayComponent,
    PaginatorComponent,
    ActionMenuComponent,
    ActionMenuButtonComponent,
    NewsItemListComponent,
    GalleryListComponent,
    DocumentListComponent,
    AddDocumentDialogComponent,
    DeleteDocumentDialogComponent,
    DeleteNewsItemDialogComponent,
    ManageRolesComponent,
    StatisticsMenuComponent,
    StatisticsOverviewComponent,
    StatisticsTodayComponent,
    StatisticsThisWeekComponent,
    StatisticsThisMonthComponent,
    StatisticsThisYearComponent,
    PrivacyComponent,
    DownloadAppComponent,
    HelpComponent,
    OpenWaterTestListComponent,
    AddOpenWaterTestComponent,
    EditOpenWaterTestComponent,
    OpenWaterTestContentDialogComponent,
    DeleteOpenWaterTestDialogComponent
]