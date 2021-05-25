import userEvent from "@testing-library/user-event";
import { UserInput } from "../../../entities/auth/models";
import { InputElement } from "../../common/inputs/input-element";

interface UserEditFromProps {
    isNew: boolean,
    user: UserInput,
    onChange: (user : UserInput) => void
}

export const UserEditForm = (props: UserEditFromProps) => {

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

    const onPasswordChange = (value: string) => {
        props.onChange({...props.user, password: value })
    }

    const onConfirmPasswordChange = (value: string) => {
        props.onChange({...props.user, confirmPassword: value })
    }
    
    return (
        <div>
            { props.isNew
                ? <></>
                : (<InputElement id={"Id"} inputType={"text"} label={"Id:"} value={props.user.id} disabled={true} onChange={onIdChange}
                />)
            }
            <InputElement id={"employeeId"} inputType={"text"} label={"Employee Id:"} value={props.user.employeeId} onChange={onEmployeeIdChange} />
            <InputElement id={"email"} inputType={"email"} label={"Email:"} value={props.user.email} onChange={onEmailChange} />
            <InputElement id={"phoneNumber"} inputType={"text"} label={"Phone Number:"} value={props.user.phoneNumber} onChange={onPhoneNumberChange} />
            <InputElement id={"pasword"} inputType={"password"} label={"Password:"} value={props.user.password} onChange={onPasswordChange} />
            <InputElement id={"confirmPasword"} inputType={"password"} label={"Confirm Password:"} value={props.user.confirmPassword} onChange={onConfirmPasswordChange} />
        </div>
    );
}
