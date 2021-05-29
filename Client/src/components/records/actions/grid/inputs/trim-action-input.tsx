import React from "react";
import { IAction, TrimAction } from "../../../../../entities/actions/models";
import { InputElement } from "../../../../common/inputs/input-element";

interface TrimActionInputProps {
    action: TrimAction,
    onActionChange: (action: IAction) => void
}

export const TrimActionInput = (props: TrimActionInputProps) => {

    const getAction = () => {
        if (!props.action || !props.action.clone)
        {
            return new TrimAction();
        }
        
        return props.action.clone();
    }

    const onStartChange = (value: string) => {
        const action = getAction();
        action.start = value;

        props.onActionChange(action);
    }
    
    const onEndChange = (value: string) => {
        const action = getAction();
        action.end = value;
        
        props.onActionChange(action);
    }
    
    const onOutputChange = (value: string) => {
        const action = getAction();
        action.outputPath = value;
        props.onActionChange(action);
    }

    return (
        <div>
            <InputElement 
                id={"startTime"}
                inputType={"text"}
                label={"Start time:"}
                value={props.action.start?.toString() ?? 0}
                onChange={onStartChange}
                />

            <InputElement 
                id={"end"}
                inputType={"text"}
                label={"End time:"}
                value={props.action.end?.toString() ?? 0}
                onChange={onEndChange}
                />

            <InputElement 
                id={"output"}
                inputType={"text"}
                label={"Output file name:"}
                value={props.action.outputPath ?? ""}
                onChange={onOutputChange}
                />
        </div>
    );
}