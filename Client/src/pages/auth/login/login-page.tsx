import React, { CSSProperties, useState } from "react"
import { Button } from "react-bootstrap";
import { InputElement } from "../../../components/common/inputs/input-element";
import { LoginModel, LoginResult } from "../../../entities/auth/models";
import { Auth } from "../../../services/auth/auth";
import { AuthService } from "../../../services/auth/auth-service";
import { Redirect } from "../../../services/navigation/redirect";

export const LoginPage = () => {
    const service = new AuthService();
    const auth = new Auth();

    const [loginModel, setLoginModel] = useState<LoginModel>({ employeeId: "", password: ""});
    const [error, setError] = useState<string | null>(null);

    const onEmployeeIdChange = (value: string) => {
        setLoginModel({
            ...loginModel,
            employeeId: value
        });
    }

    const onPasswordChange = (value: string) => {
        setLoginModel({
            ...loginModel,
            password: value
        });
    }

    const login = async () => {
        setError(null);
        const response = await service.login(loginModel);
        if (response) {
            setError(response);
        }
    }

    const containerStyles: CSSProperties = {
        width: '50%',
        margin: '30px auto'
    }

    return (
        <div style={containerStyles}>
            <h3 className="d-flex justify-content-center">Login</h3>
            <InputElement 
                id={"employeeId"}
                inputType={"text"}
                label={"Employee id:"}
                value={loginModel.employeeId}
                onChange={onEmployeeIdChange} />
            <InputElement 
                id={"password"}
                inputType={"password"}
                label={"Password:"}
                value={loginModel.password}
                onChange={onPasswordChange} />
            <div className="d-flex justify-content-center">
                <Button variant="primary" onClick={() => login()}>Login</Button>
            </div>
            { error 
                ? (<div style={{color: 'red'}}>{error}</div>)    
                : (<></>)
            }
        </div>
    );
}