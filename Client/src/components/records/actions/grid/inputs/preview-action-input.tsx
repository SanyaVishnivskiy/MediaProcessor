import React from "react";
import { GeneratePreviewAction, IAction, ResizeAction } from "../../../../../entities/actions/models";
import { ActionInputElement } from "./action-input-element";

interface PreviewActionInputProps {
    action: GeneratePreviewAction,
    recordId: string,
    onActionChange: (action: IAction) => void
}

export const PreviewActionInput = (props: PreviewActionInputProps) => {

    const getAction = (): GeneratePreviewAction => {
        if (!props.action || !props.action.clone)
        {
            const action = new GeneratePreviewAction();
            action.recordId = props.recordId;
            return action;
        }
        
        const action = props.action.clone();
        action.recordId = props.recordId;
        return action;
    }

    const onTimeOfSnapshotChange = (value: string) => {
        const action = getAction();
        action.timeOfSnapshot = value;

        props.onActionChange(action);
    }

    return (
        <div>
            <ActionInputElement 
                id={"timeOfSnapshot"}
                inputType={"text"}
                label={"Time of snapshot:"}
                value={props.action.timeOfSnapshot ?? ""}
                onChange={onTimeOfSnapshotChange}
                />
        </div>
    );
}