export interface ApiResponse {
    status: number,
    succeeded: boolean,
    error: string | null
}

export interface ApiResponseWithPayload<T> extends ApiResponse {
    payload: T | null
}

export const successResponse = (status = 200): ApiResponse => {
    return {
        status: status,
        succeeded: true,
        error: null
    }
}

export const successResponseWithPayload = <T>(payload: T, status = 200): ApiResponseWithPayload<T> => {
    return {
        status: status,
        succeeded: true,
        error: null,
        payload: payload
    }
}

export const failedResponse = (status: number, error: string): ApiResponse => {
    return {
        status: status,
        succeeded: false,
        error: error
    }
}

export const failedResponseWithPayload = <T>(status: number, error: string): ApiResponseWithPayload<T> => {
    return {
        status: status,
        succeeded: true,
        error: null,
        payload: null
    }
}