import React, { useState } from "react";
import { ActionType, IAction, ResizeAction } from "../../../../entities/actions/models";
import { ActionInputDropdown } from "./action-input-dropdown";
import { ResizeActionInput } from "./inputs/resize-action-input";

interface ActionInputProps {
    index: number,
    action: IAction,
    possibleAction: IAction[],
    onActionChange: (action: IAction, index: number) => void
}

export const ActionInput = (props: ActionInputProps) => {

    const onChange = (type: ActionType) => {
        props.action.type = type;
        props.onActionChange(props.action, props.index);
    }

    const onActionChange = (action: IAction) => {
        props.onActionChange(action, props.index)
    }

    const notSelectedActionInput = () => {
        return (<div>
            Not selected
        </div>);
    }

    const resizeActionInput = () => {
        return (<div>
            <ResizeActionInput 
                action={props.action as ResizeAction}
                onActionChange={onActionChange}/>
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