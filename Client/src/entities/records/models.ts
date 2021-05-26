export interface IRecord {
    id: string;
    fileName: string;
    description: string;
    file: IRecordFile;
    preview: IRecordFile;
    createdOn: string;
    createdBy: string;
    modifiedOn: string;
    modifiedBy: string;
}

export interface IRecordFile {
    id : string;
    fileStoreSchema: string;
    relativePath: string;
}