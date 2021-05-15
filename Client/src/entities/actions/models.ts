export interface IAction {
    type: ActionType;
    inputPath: string;
    outputPath: string;
}

export enum ActionType {
    NotSelected = 'NotSelected',
    Resize = 'Resize',
}

export interface IRunActionsRequest {
    recordId: string;
    actions: IAction[];
}

export interface IResizeAction extends IAction {

}