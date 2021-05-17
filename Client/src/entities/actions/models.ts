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

export class ResizeAction implements IAction {
    type: ActionType = ActionType.Resize
    inputPath: string = ""
    outputPath: string = ""
    height: number = 0
    width: number = 0

    clone() : ResizeAction {
        const clone = new ResizeAction();
        clone.inputPath = this.inputPath;
        clone.outputPath = this.outputPath;
        clone.height = this.height;
        clone.width = this.width;

        return clone;
    }
}