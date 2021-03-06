import userEvent from "@testing-library/user-event";
import React from "react";
import Select from "react-dropdown-select";
import { RoleName, UserInput } from "../../../entities/auth/models";
import { InputElement } from "../../common/inputs/input-element";

interface UserEditFromProps {
    isNew: boolean,
    user: UserInput,
    onChange: (user : UserInput) => void,
    readonly? : boolean
}

const possibleRoles = [
    RoleName.admin,
    RoleName.employee
];

export const UserEditForm = (props: UserEditFromProps) => {
    
    const convertToSelect = (values: string[]): {value: string, label: string}[] => {
        return values.map(x => {
            return {
                value: x,
                label: x
            };
        });
    }

    const onIdChange = () => {}

    const onEmployeeIdChange = (value: string) => {
        props.onChange({...props.user, employeeId: value })
    }

    const onEmailChange = (value: string) => {
        props.onChange({...props.user, email: value })
    }

    const onPhoneNumberChange = (value: string) => {
        props.onChange({...props.user, phoneNumber: value })
    }

    const onFirstNameChange = (value: string) => {
        props.onChange({...props.user, firstName: value })
    }

    const onLastNameChange = (value: string) => {
        props.onChange({...props.user, lastName: value })
    }

    const onPasswordChange = (value: string) => {
        props.onChange({...props.user, password: value })
    }

    const onConfirmPasswordChange = (value: string) => {
        props.onChange({...props.user, confirmPassword: value })
    }

    const onRolesChange = (values: {value: string, label: string}[])=> {
        props.onChange({...props.user, roles: values.map(x => x.value)});
    }
    
    return (
        <div>
            { props.isNew
                ? <></>
                : (<InputElement id={"Id"} inputType={"text"} label={"Id:"} value={props.user.id} disabled={true} onChange={onIdChange}
                />)
            }
            <InputElement id={"employeeId"} inputType={"text"} label={"Employee Id:"} value={props.user.employeeId} onChange={onEmployeeIdChange} disabled={props.readonly} />
            <InputElement id={"email"} inputType={"email"} label={"Email:"} value={props.user.email} onChange={onEmailChange} disabled={props.readonly}/>
            <InputElement id={"phoneNumber"} inputType={"text"} label={"Phone Number:"} value={props.user.phoneNumber} onChange={onPhoneNumberChange} disabled={props.readonly}/>
            <InputElement id={"firstName"} inputType={"text"} label={"First name:"} value={props.user.firstName} onChange={onFirstNameChange} disabled={props.readonly}/>
            <InputElement id={"lastName"} inputType={"text"} label={"Last name:"} value={props.user.lastName} onChange={onLastNameChange} disabled={props.readonly}/>
            <InputElement id={"pasword"} inputType={"password"} label={"Password:"} value={props.user.password} onChange={onPasswordChange} disabled={props.readonly}/>
            <InputElement id={"confirmPasword"} inputType={"password"} label={"Confirm Password:"} value={props.user.confirmPassword} onChange={onConfirmPasswordChange} disabled={props.readonly}/>
            <div>
                <label>Roles:</label>
                <Select
                    disabled={props.readonly}
                    options={convertToSelect(possibleRoles)}
                    values={convertToSelect(props.user.roles)}
                    multi
                    onChange={value => onRolesChange(value)} />
            </div>
        </div>
    );
}
