export interface IAction {
    type: ActionType;
}

export enum ActionType {
    Invalid = 0,

}

export interface IRunActionsRequest {
    recordId: string;
    actions: IAction[];
}