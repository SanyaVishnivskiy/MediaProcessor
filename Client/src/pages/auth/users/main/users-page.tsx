import React from "react";
import { Link } from "react-router-dom";
import { UserList } from "../../../../components/auth/users/user-list";

export const UsersPage = () => {
    return (
        <div>
            <Link to="/users/new">Create</Link>
            <hr />
            <UserList />
        </div>
    );
}