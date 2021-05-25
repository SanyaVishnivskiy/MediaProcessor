import React from 'react';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
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
        <Navbar bg="dark" variant="dark" expand="lg">
            <Navbar.Brand href="/">Media Processor</Navbar.Brand>
            <Nav className="mr-auto">
                <MenuItem name="Records" path="/"/>
                <MenuItem name="Uploads" path="/upload"/>
                <MenuItem name="Users" path="/users" hidden={!auth.isAdmin()}/>
            </Nav>
            <Form inline>
                <Button className="mr-2" onClick={openProfile} variant="outline-secondary" >Profile</Button>
                <Button onClick={logOut} variant="outline-info" >Logout</Button>
            </Form>
        </Navbar>
    );
}

