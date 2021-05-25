import * as http from "../api/http"
import { CreateUserModel, LoginModel, LoginResult, User, UserInput } from "../../entities/auth/models";
import { AxiosResponse } from "axios";
import { Auth } from "./auth";
import { ApiResponse, ApiResponseWithPayload, failedResponse, failedResponseWithPayload, successResponse, successResponseWithPayload } from "../api/models";

export class UsersService {
    uri = "users";
    auth = new Auth();

    async createUser(user: CreateUserModel) : Promise<ApiResponse> {
        try {
            const response = await http.post(this.uri, user);
            return successResponse(response.status);
        } catch (e) {
            console.log("create user error", e);
            return this.handleErrorResponseWithPayload(e);
        }
    }

    async getById(id: string) : Promise<ApiResponseWithPayload<UserInput>> {
        try {
            const response = await http.get(this.uri + "/" +  id);
            return successResponseWithPayload(response.data as UserInput, response.status);
        } catch (e) {
            console.log("get user error", e);
            return this.handleErrorResponseWithPayload(e);
        }
    }

    async update(user: UserInput) : Promise<ApiResponse> {
        try {
            const response = await http.put(this.uri + "/" + user.id, user);
            return successResponse(response.status);
        } catch (e) {
            return this.handleErrorResponse(e);
        }
    }

    async delete(userId: string) : Promise<ApiResponse> {
        try {
            const response = await http.httpDelete(this.uri + "/" + userId);
            return successResponse(response.status);
        } catch (e) {
            return this.handleErrorResponse(e);
        }
    }
    
    async search(search: string, pageSize: number): Promise<ApiResponseWithPayload<User[]>> {
        try {
            const response = await http.get(`${this.uri}?search=${search}&pageSize=${pageSize}`);
            return successResponseWithPayload(response.data as User[], response.status);
        } catch (e) {
            return this.handleErrorResponseWithPayload(e);
        }
    }

    private handleErrorResponse(e: any): ApiResponse {
        const response = e as AxiosResponse<any>;
        console.log(JSON.stringify(response));
        if (!response.status) {
            return failedResponse(500, "Internal server error");
        }
        
        return failedResponse(response.status, response.data.toString() as string);
    }

    private handleErrorResponseWithPayload<T>(e: any): ApiResponseWithPayload<T> {
        const response = e as AxiosResponse<any>;
        console.log(response);
        if (!response.status) {
            return failedResponseWithPayload(500, "Internal server error");
        }
        
        return failedResponseWithPayload(response.status, response.data as string);
    }
}