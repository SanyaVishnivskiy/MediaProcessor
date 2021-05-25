import React from "react";
import { Link } from "react-router-dom";
import { RoleName } from "../../entities/auth/models";

interface MenuItemProps {
    name: string,
    path: string,
    hidden?: boolean
}

export const MenuItem = ({name, path, hidden}: MenuItemProps) => {
    if (hidden && hidden === true) {
        return (<></>);
    }

    return (
        <li>
            <Link to={path}>{name}</Link>
        </li>
    );
}