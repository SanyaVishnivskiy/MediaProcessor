import React from "react";
import { Nav } from "react-bootstrap";
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
        <Nav.Link href={path}>{name}</Nav.Link>
    );
}