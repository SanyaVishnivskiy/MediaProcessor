import { LoginResult, Role, User } from "../../entities/auth/models";

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
}