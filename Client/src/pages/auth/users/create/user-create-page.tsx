import React, { CSSProperties, useState } from "react";
import { Button } from "react-bootstrap";
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
    firstName: "",
    lastName: "",
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

    const containerStyles: CSSProperties = {
        width: '70%',
        margin: '20px auto'
    }

    return (
        <div style={containerStyles}>
            <h1 className="d-flex justify-content-center">Create User</h1>
            <UserEditForm isNew={true} user={user} onChange={onUserChange}/>
            <div className="d-flex justify-content-center" style={{marginTop: '10px'}}>
                <Button onClick={() => create()}>Create</Button>
            </div>
            <div style={{color: 'red'}}>{error}</div>
        </div>
    );
}