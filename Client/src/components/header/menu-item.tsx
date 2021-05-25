import React from "react";
import { Link } from "react-router-dom";
import { RoleName } from "../../entities/auth/models";

interface MenuItemProps {
    name: string,
    path: string,
    roles?: RoleName[]
}

export const MenuItem = ({name, path, roles}: MenuItemProps) => {
    if (!roles) {

    }

    return (
        <li>
            <Link to={path}>{name}</Link>
        </li>
    );
}