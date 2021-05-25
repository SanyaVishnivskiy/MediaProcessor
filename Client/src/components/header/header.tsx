import React, { useState } from 'react';
import { Link } from 'react-router-dom';
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

    if (window.location.pathname === '/login') return(<></>);

    return (
        <div>
            <nav>
                <ul>
                    <MenuItem name="Records" path="/"/>
                    <MenuItem name="Uploads" path="/upload"/>
                    <MenuItem name="Users" path="/users" roles={[RoleName.admin]}/>
                </ul>
            </nav>
            <div>
                <button onClick={() => logOut()}>Logout</button>
            </div>
            <hr />
        </div>
    );
}

