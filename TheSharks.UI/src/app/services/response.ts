import { Observable, firstValueFrom } from "rxjs"
import { BaseResponse } from "src/types/BaseResponse"
import { handleError } from "./handleError"


export async function response<T>(observable: Observable<T>, responseHandler?: (r: T) => void): Promise<BaseResponse<T>> {
    try {
        const response = await firstValueFrom(observable)
        if (responseHandler) responseHandler(response);
        return { response: response }
    } catch (e) {
        return handleError(e)
    }
}