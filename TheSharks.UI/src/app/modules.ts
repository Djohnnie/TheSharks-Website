import { LayoutModule } from "@angular/cdk/layout";
import { HttpClientModule } from "@angular/common/http";
import { ReactiveFormsModule } from "@angular/forms";
import { MatButtonModule } from "@angular/material/button";
import { MatCardModule } from "@angular/material/card";
import { MatNativeDateModule } from "@angular/material/core";
import { MatDatepickerModule } from "@angular/material/datepicker";
import { MatDialogModule } from "@angular/material/dialog";
import { MatExpansionModule } from "@angular/material/expansion";
import { MatIconModule } from "@angular/material/icon";
import { MatInputModule } from "@angular/material/input";
import { MatListModule } from "@angular/material/list";
import { MatSelectModule } from "@angular/material/select";
import { MatSidenavModule } from "@angular/material/sidenav";
import { MatSnackBarModule } from "@angular/material/snack-bar";
import { MatToolbarModule } from "@angular/material/toolbar";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { JwtModule } from "@auth0/angular-jwt";
import { AppRoutingModule } from "./app-routing.module";
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatStepperModule } from '@angular/material/stepper';
import { MatBadgeModule } from '@angular/material/badge';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatTableModule } from '@angular/material/table';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { MatRadioModule } from '@angular/material/radio';
import { MatDividerModule } from '@angular/material/divider';
import { MatTreeModule } from '@angular/material/tree';
import { DragDropModule } from '@angular/cdk/drag-drop';
import { MatTabsModule } from '@angular/material/tabs';
import { MatMenuModule } from '@angular/material/menu';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { BrowserModule } from "@angular/platform-browser";
import { AppModule } from "./app.module";
import { NgParticlesModule } from "ng-particles";
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatChipsModule } from '@angular/material/chips';
import { NgChartsModule } from 'ng2-charts';

export const AppModules: AppModule[] = [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    LayoutModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgChartsModule
]

export const MatModules: AppModule[] = [
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatInputModule,
    MatDialogModule,
    MatSnackBarModule,
    MatCardModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatExpansionModule,
    MatSelectModule,
    MatCheckboxModule,
    MatProgressSpinnerModule,
    MatStepperModule,
    MatBadgeModule,
    MatTooltipModule,
    MatTableModule,
    MatStepperModule,
    MatRadioModule,
    MatDividerModule,
    MatTreeModule,
    DragDropModule,
    MatTabsModule,
    MatMenuModule,
    MatProgressBarModule,
    MatSlideToggleModule,
    MatChipsModule
]

export const OtherModules: AppModule[] = [
    JwtModule.forRoot({
        config: {
            tokenGetter: () => {
                let t = localStorage.getItem("token")

                if (t !== null) {
                    return t
                } else {
                    return sessionStorage.getItem("token")
                }
            }
        }
    }),
    CKEditorModule,
    NgParticlesModule
]