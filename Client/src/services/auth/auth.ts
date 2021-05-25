import { LoginResult, Role, RoleName, User } from "../../entities/auth/models";
import jwt_decode from "jwt-decode";
import { AuthToken } from "./auth-token";

export class Auth {
    tokenKey = "token";
    userKey = "user";
    rolesKey = "roles"

    storeToken(result: LoginResult): void {
        localStorage.setItem(this.tokenKey, JSON.stringify(result));
    }

    getToken() : LoginResult | null {
        const value = localStorage.getItem(this.tokenKey);
        if (!value)
            return null;

        return JSON.parse(value) as LoginResult;
    }

    deleteToken(): void {
        localStorage.removeItem(this.tokenKey);
    }

    isAuthenticated(): boolean {
        const value = this.getToken();
        return (value?.token?.length ?? 0) > 0;
    }

    storeUser(user: User): void {
        localStorage.setItem(this.userKey, JSON.stringify(user));
    }

    getUser(): User | null {
        const user = localStorage.getItem(this.userKey);
        if (!user)
            return null;

        return JSON.parse(user) as User; 
    }

    getRoles() : Role[] | null {
        const value = localStorage.getItem(this.rolesKey);
        if (!value)
            return null;
        
        return JSON.parse(value) as Role[];
    }

    getParsedToken() : AuthToken | null {
        const token = this.getToken();
        if (!token)
            return null;

        return new AuthToken(jwt_decode(token?.token));
    }

    isAdmin(): boolean {
        const roles = this.getParsedToken()?.getRoles();
        return roles?.some(x => x === RoleName.admin) ?? false;
    }
}