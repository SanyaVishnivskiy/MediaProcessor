export interface IAction {
    type: ActionType;
    inputPath: string;
    outputPath: string;
}

export enum ActionType {
    NotSelected = 'NotSelected',
    Resize = 'Resize',
    GeneratePreview = 'GeneratePreview',
    Crop = 'Crop',
    Trim = 'Trim'
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

export class GeneratePreviewAction implements IAction {
    type: ActionType = ActionType.GeneratePreview
    inputPath: string = ""
    outputPath: string = ""
    recordId: string = ""
    timeOfSnapshot: string = ""

    clone() : GeneratePreviewAction {
        const clone = new GeneratePreviewAction();
        clone.inputPath = this.inputPath;
        clone.outputPath = this.outputPath;
        clone.recordId = this.recordId;
        clone.timeOfSnapshot = this.timeOfSnapshot;

        return clone;
    }
}

export class CropAction implements IAction {
    type: ActionType = ActionType.Crop
    inputPath: string = ""
    outputPath: string = ""
    height: number = 0
    width: number = 0
    x: number = 0
    y: number = 0

    clone() : CropAction {
        const clone = new CropAction();
        clone.inputPath = this.inputPath;
        clone.outputPath = this.outputPath;
        clone.height = this.height;
        clone.width = this.width;
        clone.x = this.x;
        clone.y = this.y;

        return clone;
    }
}

export class TrimAction implements IAction {
    type: ActionType = ActionType.Trim
    inputPath: string = ""
    outputPath: string = ""
    start: string = ""
    end: string = ""

    clone() : TrimAction {
        const clone = new TrimAction();
        clone.inputPath = this.inputPath;
        clone.outputPath = this.outputPath;
        clone.start = this.start;
        clone.end = this.end;

        return clone;
    }
}
