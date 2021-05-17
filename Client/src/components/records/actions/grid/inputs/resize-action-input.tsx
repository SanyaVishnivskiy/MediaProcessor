import React from "react";
import { IAction, ResizeAction } from "../../../../../entities/actions/models";
import { ActionInputElement } from "./action-input-element";

interface ResizeActionInputProps {
    action: ResizeAction,
    onActionChange: (action: IAction) => void
}

export const ResizeActionInput = (props: ResizeActionInputProps) => {

    const getAction = () => {
        if (!props.action || !props.action.clone)
        {
            return new ResizeAction();
        }
        
        return props.action.clone();
    }

    const onHeightChange = (value: string) => {
        let newValue = !value ? 0 : +value;

        const action = getAction();
        action.height = newValue;

        props.onActionChange(action);
    }
    
    const onWidthChange = (value: string) => {
        let newValue = !value ? 0 : +value;

        const action = getAction();
        action.width = newValue;
        
        props.onActionChange(action);
    }
    
    const onOutputChange = (value: string) => {
        const action = getAction();
        action.outputPath = value;
        props.onActionChange(action);
    }

    return (
        <div>
            <ActionInputElement 
                id={"height"}
                inputType={"number"}
                label={"Height:"}
                value={props.action.height?.toString() ?? 0}
                onChange={onHeightChange}
                />

            <ActionInputElement 
                id={"width"}
                inputType={"number"}
                label={"Width:"}
                value={props.action.width?.toString() ?? 0}
                onChange={onWidthChange}
                />

            <ActionInputElement 
                id={"output"}
                inputType={"text"}
                label={"Output file name:"}
                value={props.action.outputPath ?? ""}
                onChange={onOutputChange}
                />
        </div>
    );
}