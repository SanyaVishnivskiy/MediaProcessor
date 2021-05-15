import { ActionType, IAction } from "../../../../entities/actions/models";
import Select from "react-dropdown-select";

interface ActionInputDropdownProps {
    actions: IAction[]
    onChange: (type: ActionType) => void
}

export const ActionInputDropdown = (props: ActionInputDropdownProps) => {

    const getActionTypes = (): {value: string, label: string}[] => {
        return props.actions.map(x => {
            const type = ActionType[x.type];
            console.log(type);
            return {
                value: type,
                label: type
            };
        });
    } 

    const onChange = (selected: {value: string, label: string}[]) => {
        const result = ActionType[selected[0].value as keyof typeof ActionType];
        props.onChange(result);
    }

    return (
        <Select
            options={getActionTypes()}
            values={[]}
            onChange={onChange}
            />
    );
}