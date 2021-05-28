import React, { CSSProperties, useState } from "react";
import { ActionType, CropAction, GeneratePreviewAction, IAction, ResizeAction } from "../../../../entities/actions/models";
import { IRecord } from "../../../../entities/records/models";
import { ActionInputDropdown } from "./action-input-dropdown";
import { CropActionInput } from "./inputs/crop-action-input";
import { PreviewActionInput } from "./inputs/preview-action-input";
import { ResizeActionInput } from "./inputs/resize-action-input";

interface ActionInputProps {
    record: IRecord,
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

    const generatePreviewInput = () => {
        return (
            <div>
                <PreviewActionInput 
                    action={props.action as GeneratePreviewAction}
                    onActionChange={onActionChange}
                    recordId={props.record.id}/>
            </div>
        );
    }

    const generateCropInput = () => {
        return (
            <div>
                <CropActionInput 
                    action={props.action as CropAction}
                    onActionChange={onActionChange} />
            </div>
        );
    }

    const renderInputFor = (type: ActionType) => {
        switch (props.action.type) {
            case ActionType.NotSelected:
                return notSelectedActionInput();
            case ActionType.Resize:
                return resizeActionInput();
            case ActionType.GeneratePreview:
                return generatePreviewInput();
            case ActionType.Crop:
                return generateCropInput();
            default:
                return notSelectedActionInput();
        };
    }

    const containerStyles: CSSProperties = {
        padding: '10px',
        marginBottom: '10px'
    }

    return (
        <div className="rounded border border-secondary" style={containerStyles}>
            <ActionInputDropdown
                actions={props.possibleAction}
                onChange={onChange}
            />
            {renderInputFor(props.action.type)}
        </div>
    );
}