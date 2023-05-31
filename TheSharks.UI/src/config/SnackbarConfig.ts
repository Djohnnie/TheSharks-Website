import { MatSnackBarConfig } from "@angular/material/snack-bar";

export function snackbarConfig(severity?: 'error' | 'success'): MatSnackBarConfig<any> {
    return {
        duration: 3000,
        horizontalPosition: "center",
        panelClass: `sb-${severity}`
    }
}   