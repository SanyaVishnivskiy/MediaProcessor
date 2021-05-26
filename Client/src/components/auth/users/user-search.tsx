import React, { CSSProperties, useEffect, useState } from "react";
import { Button, Col, Container, Form, Row } from "react-bootstrap";
import { User, UserInput } from "../../../entities/auth/models";
import { UsersService } from "../../../services/auth/users-service";
import { Redirect } from "../../../services/navigation/redirect";
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

    const redirectToCreate = () => {
        Redirect.toUserCreation();
    }

    useEffect(() => {
        searchUsers();
    }, [])

    const searchStyle: CSSProperties = {
        width: '90%',
        margin: '0 auto'
    }

    return (
        <div  style={searchStyle}>
            <Container>
                <Row>
                    <Col sm="8">
                        <Form.Group controlId={"search"} as={Row}>
                            <Form.Control
                                type={"text"}
                                name={"search"} 
                                onChange={e => onChange(e.target.value)}
                                value={search} />
                        </Form.Group>
                    </Col>
                    <Col sm="2">
                        <Button variant="primary" block onClick={() => searchUsers()}>Search</Button>
                    </Col>
                    <Col sm="2">
                        <Button variant="success" block onClick={() => redirectToCreate()}>Create</Button>
                    </Col>
                </Row>
            </Container>
            <UserList users={users}/>
        </div>
    )
}