import { create } from "node:domain";
import React, { useState } from "react";
import { UserEditForm } from "../../../../components/auth/users/user-edit-form";
import { CreateUserModel, RoleName, UserInput } from "../../../../entities/auth/models";
import { Auth } from "../../../../services/auth/auth";
import { AuthService } from "../../../../services/auth/auth-service";
import { UsersService } from "../../../../services/auth/users-service";
import { Redirect } from "../../../../services/navigation/redirect";

const emptyUser : CreateUserModel = {
    id: "",
    employeeId: "",
    email: "",
    phoneNumber: "",
    password: "",
    confirmPassword: "",
    roles: [ RoleName.employee ]
}

export const UserCreatePage = () => {
    const service = new UsersService();
    const auth = new Auth();

    const [user, setUser] = useState<CreateUserModel>(emptyUser);
    const [error, setError] = useState<string | null>(null);

    const onUserChange = (user: UserInput) : void => {
        setUser(user);
    }

    const create = async () => {
        const response = await service.createUser(user);
        if (!response.succeeded)
        {
            setError(response.error);
            return;
        }
        Redirect.toUsers();
    }
    
    if (!auth.isAdmin()) {
        Redirect.to("/");
    }

    return (
        <div>
            <UserEditForm isNew={true} user={user} onChange={onUserChange}/>
            <button onClick={() => create()}>Create</button>
            <div style={{color: 'red'}}>{error}</div>
        </div>
    );
}