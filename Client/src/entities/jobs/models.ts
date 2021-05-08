export interface IJob {
    id?: string;
    type: JobType;
    data: any;
}

export enum JobType {
    Unknown,
    Actions
}

export interface IServerJob {
    id?: string;
    type: string;
    data: any;
}