import React from "react";
import { User } from "../../../entities/auth/models";
import { UserListRow } from "./user-list-row";
import "./users.css"

interface UserListProps {
    users: User[]
}

export const UserList = ({users}: UserListProps) => {

    if (users.length === 0) {
        return (
            <h3>No users found</h3>
        );
    }

    return (
        <div>
            <table className="user-list">
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Employee Id</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                {
                    users.map((u, i) => {
                        return (<UserListRow key={i} user={u}/>);
                    })
                }
                </tbody>
            </table>
            
        </div>
    );
}