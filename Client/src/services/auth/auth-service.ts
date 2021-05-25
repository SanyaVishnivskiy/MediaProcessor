import * as http from "../api/http"
import { CreateUserModel, LoginModel, LoginResult } from "../../entities/auth/models";
import { AxiosResponse } from "axios";
import { Auth } from "./auth";
import { Redirect } from "../navigation/redirect";

export class AuthService {
    uri = "auth";
    auth = new Auth();

    async login(model: LoginModel): Promise<string | null> {
        const response = await this.getToken(model);
        if (!(response as LoginResult)?.token) {
            return response.toString();
        }

        this.auth.storeToken(response as LoginResult);
        //get user and roles
        Redirect.to("/");
        return null;
    }

    private async getToken(model: LoginModel): Promise<LoginResult | string> {
        try {
            const response = await http.post(this.uri + "/token", model);
            return response.data as LoginResult;
        } catch (e) {
            console.log(e);
            return this.handleErrorResponse(e);
        }
    }

    private handleErrorResponse(e: any): string {
        const response = e as AxiosResponse<any>;
        console.log(response);
        if (!response.status) {
            return "Internal server error";
        }
        
        return response.data as string;
    }
}