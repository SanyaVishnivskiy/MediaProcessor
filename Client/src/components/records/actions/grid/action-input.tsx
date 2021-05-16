import React, { useState } from "react";
import { ActionType, IAction } from "../../../../entities/actions/models";
import { ActionInputDropdown } from "./action-input-dropdown";

interface ActionInputProps {
    index: number,
    action: IAction,
    possibleAction: IAction[],
    onActionChange: (action: IAction, index: number) => void
}

export const ActionInput = (props: ActionInputProps) => {
    //const [action, setAction] = useState<IAction>(props.action);// ?

    const onChange = (type: ActionType) => {
        props.action.type = type;
        //setAction(action);// ?
        props.onActionChange(props.action, props.index);
    }

    const notSelectedActionInput = () => {
        return (<div>
            Not selected
        </div>);
    }

    const resizeActionInput = () => {
        return (<div>
            Resize
        </div>);
    }

    const renderInputFor = (type: ActionType) => {
        switch (props.action.type) {
            case ActionType.NotSelected:
                return notSelectedActionInput();
            case ActionType.Resize:
                return resizeActionInput();
        };
    }

    return (
        <div>
            <ActionInputDropdown
                actions={props.possibleAction}
                onChange={onChange}
            />
            {renderInputFor(props.action.type)}
        </div>
    );
}