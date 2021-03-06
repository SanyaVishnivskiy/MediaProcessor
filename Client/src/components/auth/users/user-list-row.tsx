import { User } from "../../../entities/auth/models";
import { Redirect } from "../../../services/navigation/redirect";

interface UserListRowProps {
    user: User
}

export const UserListRow = ({user}: UserListRowProps) => {
    const redirect = () => {
        Redirect.toUser(user.id);
    }
    
    return(
        <tr onClick={(() => redirect())}>
            <td>{user.employeeId}</td>
            <td>{`${user.firstName} ${user.lastName}`}</td>
            <td>{user.email}</td>
        </tr>
    );
}