import React, { useState } from 'react';
import { RoleName } from '../../entities/auth/models';
import { Auth } from '../../services/auth/auth';
import { Redirect } from '../../services/navigation/redirect';
import { MenuItem } from './menu-item';

interface HeaderProps {
}

export const Header = (props: HeaderProps) => {
    const auth = new Auth();

    const logOut = () => {
        auth.deleteToken();
        Redirect.toLogin();
    }

    const openProfile = async () => {
        const token = auth.getParsedToken();
        if (!token) {
            logOut();
            return;
        }

        const id = token.getId();
        Redirect.toUser(id);
    }

    if (window.location.pathname === '/login') return(<></>);

    return (
        <div>
            <nav>
                <ul>
                    <MenuItem name="Records" path="/"/>
                    <MenuItem name="Uploads" path="/upload"/>
                    <MenuItem name="Users" path="/users" hidden={!auth.isAdmin()}/>
                </ul>
            </nav>
            <div>
                <button onClick={() => openProfile()}>Profile</button>
                <button onClick={() => logOut()}>Logout</button>
            </div>
            <hr />
        </div>
    );
}

