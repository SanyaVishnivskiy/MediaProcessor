import React from "react";
import { Button } from "react-bootstrap";
import { UserSearch } from "../../../../components/auth/users/user-search";
import { Auth } from "../../../../services/auth/auth";
import { Redirect } from "../../../../services/navigation/redirect";

export const UsersPage = () => {
    const auth = new Auth();
    
    if (!auth.isAdmin()) {
        Redirect.to("/");
    }

    return (
        <div style={{marginTop: '20px'}}>
            <UserSearch />
        </div>
    );
}