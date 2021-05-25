import React, { useEffect, useState } from "react";
import { RouteComponentProps } from "react-router-dom";
import { UserEditForm } from "../../../../components/auth/users/user-edit-form";
import { CreateUserModel, UserInput } from "../../../../entities/auth/models";
import { UsersService } from "../../../../services/auth/users-service";

interface UserEditPageRouteParams {
    id: string
}

interface UserEditPageProps extends RouteComponentProps<UserEditPageRouteParams> {
}

export const UserEditPage = (props: UserEditPageProps) => {
    const service = new UsersService();
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

        if (!response.succeeded && response.status === 404) {
            setFetchError("Not found or you don't have access to see this page");
            return;
        }

        if (!response.succeeded)
        {
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
        console.log(response);
        if (!response.succeeded)
        {
            setError(response.error);
            return;
        }

        setSaveResponse("Saved successfully");
    }

    useEffect(() => {
        fetchUser();
    },[])

    if (!user && !fetchError)
        return (
            <h3>Fetching...</h3>
        );

    if (fetchError) {
        <h2 style={{color: 'red'}}>{fetchError}</h2>
    }

    return (
        <div>
            <UserEditForm isNew={false} user={user as UserInput} onChange={onUserChange}/>
            <button onClick={() => save()}>Save</button>
            <div style={{color: 'red'}}>{error}</div>
            <div style={{color: 'green'}}>{saveResponse}</div>
        </div>
    );
}