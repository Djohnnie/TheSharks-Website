import { ErrorResponse } from "./ErrorResponse";

export interface BaseResponse<T> {
    response?: T;
    error?: ErrorResponse;
}