export interface IRecord {
    id: string;
    fileName: string;
    file: IRecordFile;
    preview: IRecordFile;
}

export interface IRecordFile {
    id : string;
    fileStoreSchema: string;
    relativePath: string;
}