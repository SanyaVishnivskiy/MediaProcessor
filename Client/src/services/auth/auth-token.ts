import { collapseTextChangeRangesAcrossMultipleVersions } from "typescript";

export class AuthToken {
    token: {
        "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": string
        "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string[] | string
    };

    constructor(token: any) {
        this.token = token;
    }

    getId() : string {
        return this.token["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"] as string;
    }

    getRoles(): string[] {
        const value = this.token["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];
        if (Array.isArray(value)) {
            return value as string[];
        }

        return [ value as string ];
    }
}