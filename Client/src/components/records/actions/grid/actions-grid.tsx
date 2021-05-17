import React, { useEffect, useState } from "react";
import { ActionType, IAction } from "../../../../entities/actions/models";
import { ActionInput } from "./action-input";

interface ActionsGridProps {
    possibleActions: IAction[]
    selectedActions: IAction[]
    onSelectedActionsChange: (selectedActions: IAction[]) => void
}

const emptyAction: IAction = {
    type: ActionType.NotSelected,
    inputPath: '',
    outputPath: ''
};

export const ActionsGrid = (props: ActionsGridProps) => {
    const [actions, setActions] = useState<IAction[]>(new Array<IAction>());

    const addNewActionInput = () => {
        actions.push(emptyAction);
        setActions(actions);
        props.onSelectedActionsChange(actions);
    }

    const onActionChange = (action: IAction, index: number): void => {
        actions[index] = action;
        
        props.onSelectedActionsChange(actions);
    }

    useEffect(() => {
        if (actions.length === 0)
            addNewActionInput();
    }, [])

    return (
        <div>
            {actions && actions.length > 0
            ? actions.map((a, i) => {
                return <ActionInput
                    key={i}
                    index={i}
                    action={a}
                    possibleAction={props.possibleActions}
                    onActionChange={onActionChange}
                    />
            })
            : <h3>No actions available!</h3>
            }
        </div>
    );
}