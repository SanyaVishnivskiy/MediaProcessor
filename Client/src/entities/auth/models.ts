export interface LoginModel {
    employeeId: string;
    password: string;
}

export interface LoginResult {
    employeeId: string;
    token: string;
}

export interface CreateUserModel {
    id: string;
    employeeId: string;
    phoneNumber: string;
    email: string;
    password: string;
    confirmPassword: string;
}

export interface User {
    id: string;
    employeeId: string;
    phoneNumber: string;
    email: string;
}

export interface UserInput extends CreateUserModel{

}

export enum RoleName {
    none = "none",
    admin = "admin",
}

export interface Role {
    name: RoleName;
    id: string;
}

//export const toUser(input:)