import userEvent from "@testing-library/user-event";
import React, { useEffect, useState } from "react";
import { RouteComponentProps } from "react-router-dom";
import { UserEditForm } from "../../../../components/auth/users/user-edit-form";
import { CreateUserModel, UserInput } from "../../../../entities/auth/models";
import { Auth } from "../../../../services/auth/auth";
import { UsersService } from "../../../../services/auth/users-service";
import { Redirect } from "../../../../services/navigation/redirect";

interface UserEditPageRouteParams {
    id: string
}

interface UserEditPageProps extends RouteComponentProps<UserEditPageRouteParams> {
}

export const UserEditPage = (props: UserEditPageProps) => {
    const service = new UsersService();
    const auth = new Auth();
    const id = props.match.params.id;

    const [user, setUser] = useState<CreateUserModel | null>(null);
    const [error, setError] = useState<string | null>(null);
    const [fetchError, setFetchError] = useState<string | null>(null);
    const [saveResponse, setSaveResponse] = useState<string | null>(null);

    const onUserChange = (user: UserInput) : void => {
        setUser(user);
    }

    const fetchUser = async () => {
        setFetchError(null);
        const response = await service.getById(id);
        if (response.status === 404 || response.status === 403) {
            setFetchError("Not found or you don't have access to see this page");
            return;
        }

        if (!response.succeeded) {
            setFetchError(response.error);
            return;
        }

        setUser(response.payload);
    }

    const save = async () => {
        if (user === null)
            return;

        setSaveResponse(null);
        setError(null);

        const response = await service.update(user);
        if (!response.succeeded)
        {
            setError(response.error);
            return;
        }

        setSaveResponse("Saved successfully");
    }

    const deleteUser = async () => {
        if (!user || !user?.id)
            return;
        
        const response = await service.delete(user.id);
        if (!response.succeeded) {
            setError(response.error);
            return;
        }

        Redirect.toUsers();
    }

    useEffect(() => {
        fetchUser();
    },[])

    if (!user && !fetchError)
        return (
            <h3>Fetching...</h3>
        );

    if (fetchError) {
        return (<h2 style={{color: 'red'}}>{fetchError}</h2>);
    }

    const getUserState = () : CreateUserModel => {
        return {
            id: user?.id ?? "",
            employeeId: user?.employeeId ?? "",
            phoneNumber: user?.phoneNumber ?? "",
            email: user?.email ?? "",
            password: user?.password ?? "",
            confirmPassword: user?.confirmPassword ?? "",
            roles: user?.roles ?? auth.getParsedToken()?.getRoles() ?? []
        }
    }

    if (!auth.isAdmin()){
        return (
            <div>
                <UserEditForm isNew={false} user={getUserState() as UserInput} onChange={onUserChange} readonly={true}/>
            </div>
        );
    }

    return (
        <div>
            <UserEditForm isNew={false} user={getUserState() as UserInput} onChange={onUserChange}/>
            <button onClick={() => save()}>Save</button>
            <button onClick={() => deleteUser()}>Delete</button>
            <div style={{color: 'red'}}>{error}</div>
            <div style={{color: 'green'}}>{saveResponse}</div>
        </div>
    );
}