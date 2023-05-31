import { HttpErrorResponse } from "@angular/common/http";
import { BaseResponse } from "src/types/BaseResponse";
import { ErrorMessageDto } from "src/types/dto/ErrorMessageDto";

export function handleError(error: unknown): BaseResponse<any> {
    const e = error as HttpErrorResponse

    return {
        error: {
            code: e.status,
            message: (e.error as ErrorMessageDto).message
        }
    }
}