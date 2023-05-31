import { APP_INITIALIZER, NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { AuthService } from './services/auth/auth.service';
import { AppModules, MatModules, OtherModules } from './modules';
import { AppComponents } from './components';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { JwtInterceptor } from './interceptors/jwt-interceptor.injectable';
import { DeleteEnrollmentDialogComponent } from './activity/delete-enrollment-dialog/delete-enrollment-dialog.component';
import { LandingScreenComponent } from './landing-screen/landing-screen.component';
import { DeleteRoleDialogComponent } from './role/delete-role-dialog/delete-role-dialog.component';

function jwtCheck(as: AuthService) {
    //Check for sessions if user has token and if token is not expired
    return () => {
        as.validateToken()
    }
}

@NgModule({
    declarations: [
        AppComponents,
        DeleteEnrollmentDialogComponent,
        LandingScreenComponent,
        DeleteRoleDialogComponent,
    ],
    imports: [
        AppModules,
        MatModules,
        OtherModules
    ],
    providers: [
        {
            provide: APP_INITIALIZER,
            useFactory: jwtCheck,
            deps: [AuthService],
            multi: true
        },
        {
            provide: HTTP_INTERCEPTORS,
            useClass: JwtInterceptor,
            multi: true
        }
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
