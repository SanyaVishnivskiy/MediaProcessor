import React from "react";
import { CropAction, IAction, ResizeAction } from "../../../../../entities/actions/models";
import { InputElement } from "../../../../common/inputs/input-element";

interface CropActionInputProps {
    action: CropAction,
    onActionChange: (action: IAction) => void
}

export const CropActionInput = (props: CropActionInputProps) => {

    const getAction = () => {
        if (!props.action || !props.action.clone)
        {
            return new CropAction();
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
    
    const onXChange = (value: string) => {
        let newValue = !value ? 0 : +value;

        const action = getAction();
        action.x = newValue;

        props.onActionChange(action);
    }
    
    const onYChange = (value: string) => {
        let newValue = !value ? 0 : +value;

        const action = getAction();
        action.y = newValue;
        
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
                id={"height"}
                inputType={"number"}
                label={"Height:"}
                value={props.action.height?.toString() ?? 0}
                onChange={onHeightChange}
                />

            <InputElement 
                id={"width"}
                inputType={"number"}
                label={"Width:"}
                value={props.action.width?.toString() ?? 0}
                onChange={onWidthChange}
                />

            <InputElement 
                id={"x"}
                inputType={"number"}
                label={"Start horizontal position:"}
                value={props.action.x?.toString() ?? 0}
                onChange={onXChange}
                />

            <InputElement 
                id={"y"}
                inputType={"number"}
                label={"Start vertical position:"}
                value={props.action.y?.toString() ?? 0}
                onChange={onYChange}
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