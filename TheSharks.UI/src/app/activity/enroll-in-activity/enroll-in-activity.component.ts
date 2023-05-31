import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Component, OnInit, ViewChild } from '@angular/core';
import { UntypedFormArray, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatStepper } from '@angular/material/stepper';
import { MatTable } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';
import { firstValueFrom, map, Observable, shareReplay } from 'rxjs';
import { EnrollmentService } from 'src/app/services/enrollment/enrollment.service';
import { MemberService } from 'src/app/services/member/member.service';
import { ActivityService } from 'src/app/services/activity/activity.service';
import { UserService } from 'src/app/services/user/user.service';
import { RoleService } from 'src/app/services/role/role.service';
import { OpenWaterTestService } from 'src/app/services/open-water-test/open-water-test.service';
import { OpenWaterTestDto } from 'src/types/dto/open-water-test/OpenWaterTestDto';
import { snackbarConfig } from 'src/config/SnackbarConfig';
import { EnrollmentGuest } from 'src/types/application/enrollment/EnrollmentGuest';
import { EnrollmentMember } from 'src/types/application/enrollment/EnrollmentMember';
import { MemberListItem } from 'src/types/application/member/MemberListItem';
import { User } from 'src/types/application/user/User';
import { Activity } from 'src/types/application/activity/Activity';
import { DiveCertificateDto } from 'src/types/dto/role/DiveCertificateDto';
import { MemberListDialogComponent } from '../member-list-dialog/member-list-dialog.component';
import { OpenWaterTestContentDialogComponent } from '../../open-water-test/open-water-test-content-dialog/open-water-test-content-dialog.component'

@Component({
    selector: 'app-enroll-in-activity',
    templateUrl: './enroll-in-activity.component.html',
    styleUrls: ['./enroll-in-activity.component.scss']
})
export class EnrollInActivityComponent implements OnInit {

    private _id = "";
    private _type = "";
    private _user: User | undefined
    private _memberList: MemberListItem[] | undefined
    private _filteredMemberList: MemberListItem[] | undefined
    
    activity: Activity | undefined
    openWaterTests: OpenWaterTestDto[] | undefined
    isDiveActivity: boolean | undefined
    isOpenWaterTest: boolean = false

    isHandset$: Observable<boolean> = this._bo.observe([Breakpoints.Small, Breakpoints.XSmall])
        .pipe(
            map(result => result.matches),
            shareReplay()
        );

    diveCertificatesList: DiveCertificateDto[] | undefined
    loading = false;

    guestEnrollments: EnrollmentGuest[] = []
    memberEnrollments: EnrollmentMember[] = []
    membersWithDiveCertificate: Map<string,OpenWaterTestDto[] | undefined> = new Map()
    memberColumns = ["name", "asDiver"]
    membersForm = new UntypedFormGroup({
        members: new UntypedFormArray([]),
        remarks: new UntypedFormControl()
    })
    get members() {
        return this.membersForm.get("members") as UntypedFormArray
    }
    get remarks() {
        return this.membersForm.get("remarks") as UntypedFormArray
    }

    @ViewChild("enrollmentsTable")
    enrollmentsTable: MatTable<EnrollmentGuest> | undefined;

    @ViewChild("stepper")
    stepper!: MatStepper

    constructor(
        private _route: ActivatedRoute,
        private _es: EnrollmentService,
        private _us: UserService,
        private _as: ActivityService,
        private _router: Router,
        private _ms: MemberService,
        private _rs: RoleService,
        private _ows: OpenWaterTestService,
        public dialog: MatDialog,
        private _sb: MatSnackBar,
        private _bo: BreakpointObserver
    ) {
        
    }

    async ngOnInit() {
        const params = await firstValueFrom(this._route.params)
        this._id = params["id"]
        this._type = params["type"]

        this._user = (await this._us.getUser()).response
        this.diveCertificatesList = (await this._rs.getDiveCertificates()).response
        this.activity = (await this._as.getActivity(this._id, this._type)).response
        this.isDiveActivity = this.activity?.activityType === "dive"
        this.openWaterTests = (await this._ows.getAllOpenWaterTests("")).response?.openWaterTests

        const openWaterDiveCertificates = Array.from(new Set(this.openWaterTests?.map(x => x.diveCertificate)))
        const membersWithRoles = (await this._ms.getAllMembersWithRoles()).response;
        membersWithRoles?.forEach( member => {
            const diveCertificate = member.roles.filter( role => openWaterDiveCertificates.includes(role.name) ).map( x => x.name )[0]
            const openWaterTests = this.openWaterTests?.filter( x => x.diveCertificate === diveCertificate ) ?? []
            this.membersWithDiveCertificate.set(member.id, openWaterTests?.length > 0 ? openWaterTests : this.openWaterTests )            
        })

        this.addMember({
            id: this._user!.id,
            role: "",
            firstName: this._user!.firstName,
            lastName: this._user!.lastName,
        });
    }

    async onSubmit() {
        this.loading = true
        this.memberEnrollments = [];
        this.guestEnrollments = [];

        this.members.controls.forEach(member => {
            if( member.value.isMember ) {
                this.memberEnrollments.push({
                    registreeId: member.value.registreeId,
                    registree: member.value.name,
                    asDiver: member.value.asDiver,
                    remark: this.remarks.value,
                    openWaterTestId: member.value.openWaterTestId
                })
            } else {
                this.guestEnrollments.push({
                    registree: member.value.registree,
                    asDiver: member.value.asDiver,
                    diveCertificate: member.value.diveCertificate,
                    remark: this.remarks.value,
                    openWaterTestId: member.value.openWaterTestId
                })
            }
        });

        const response = await this._es.addEnrollments(this._id, this._user!.id, this.memberEnrollments, this.guestEnrollments)
        if (response.error === undefined) {
            this._router.navigateByUrl(`/activities/${this._type}/${this._id}`)
            this._sb.open("Inschrijving gelukt", "", snackbarConfig("success"))
        } else {
            this._sb.open(response.error.message, "", snackbarConfig("error"))
        }

        this.loading = false
    }

    addGuest() {
        this.members.push(new UntypedFormGroup({
            isMember: new UntypedFormControl(false, [Validators.required]),
            registree: new UntypedFormControl("", [Validators.required]),
            diveCertificate: new UntypedFormControl("", [Validators.required]),
            asDiver: new UntypedFormControl(false, [Validators.required]),
            openWaterTestId: new UntypedFormControl(null)
        }))
    }

    async chooseMember() {
        //Don't fetch if present
        if (!this._memberList && this._user) {
            this._memberList = (await this._ms.getMembersAndDiveLabels(this._user.id)).response?.members
        }

        const enrolledIds = this.activity?.enrollments.map(enrollment => enrollment.id) ?? [];
        const usedIds = this.members.controls.map(member => member.value.registreeId);
        this._filteredMemberList = this._memberList?.filter(member => !enrolledIds.includes(member.id));
        this._filteredMemberList = this._filteredMemberList?.filter(member => !usedIds.includes(member.id));

        const ref = this.dialog.open(MemberListDialogComponent, {
            data: this._filteredMemberList
        });

        const result = await firstValueFrom(ref.afterClosed())

        //Check result from dialog
        if (result !== undefined) {
            this.addMember(result)
        }
    }

    private addMember(member: MemberListItem) {
        this.members.push(new UntypedFormGroup({
            isMember: new UntypedFormControl(true, [Validators.required]),
            registreeId: new UntypedFormControl(member.id, [Validators.required]),
            name: new UntypedFormControl(member.firstName + " " + member.lastName, [Validators.required]),
            asDiver: new UntypedFormControl(true, [Validators.required]),
            openWaterTestId: new UntypedFormControl(null)
        }))
    }

    deleteEnrollment(index: number) {
        this.members.removeAt(index)
    }

    getOpenWaterTestsForMember(id: string) {
        return this.membersWithDiveCertificate.get(id)
    }

    loadOpenWaterTestInfo(event: Event, openWaterTestId: string) {
        event.stopPropagation();

        const openWaterTest = this.openWaterTests?.find( x => x.id === openWaterTestId )

        if( openWaterTest ) {
            this.dialog.open(OpenWaterTestContentDialogComponent, {
                data: { 
                    title: openWaterTest.title,
                    content: openWaterTest.content
                }
            })
        }
    }
}