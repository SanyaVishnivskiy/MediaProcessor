import React from "react";
import { Link } from "react-router-dom";
import { UserList } from "../../../../components/auth/users/user-list";
import { Auth } from "../../../../services/auth/auth";
import { Redirect } from "../../../../services/navigation/redirect";

export const UsersPage = () => {
    const auth = new Auth();
    
    if (!auth.isAdmin()) {
        Redirect.to("/");
    }

    return (
        <div>
            <Link to="/new/users">Create</Link>
            <hr />
            <UserList />
        </div>
    );
}