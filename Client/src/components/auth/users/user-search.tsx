import React, { useEffect, useState } from "react";
import { User, UserInput } from "../../../entities/auth/models";
import { UsersService } from "../../../services/auth/users-service";
import { InputElement } from "../../common/inputs/input-element"
import { UserList } from "./user-list";

export const UserSearch = () => {
    const service = new UsersService();
    const maxUserCount = 10;

    const [search, setSearch] = useState("");
    const [users, setUsers] = useState(new Array<User>());

    const searchUsers = async () => {
        const response = await service.search(search, maxUserCount);
        if (response.succeeded) {
            setUsers(response.payload ?? new Array<User>());
            return;
        }
        console.log(users);
    }

    const onChange = (value: string) => {
        setSearch(value);
    }

    useEffect(() => {
        searchUsers();
    }, [])

    return (
        <div>
            <InputElement id="search" label={"Search:"} value={search} inputType={"text"} onChange={onChange}/>
            <button onClick={() => searchUsers()}>Search</button>
            <UserList users={users}/>
        </div>
    )
}