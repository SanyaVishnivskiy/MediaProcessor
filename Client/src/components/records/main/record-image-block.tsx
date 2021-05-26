import { IRecord } from "../../../entities/records/models";
import * as http from "../../../services/api/http"
import * as filesApi from "../../../services/api/files-urls"
import { CSSProperties } from "react";

interface RecordImageBlock {
    record: IRecord,
    height?: string,
    width?: string,
    style?: CSSProperties
}

export const RecordImageBlock = ({record, height, width, style}: RecordImageBlock) => {
    const getFilePath = () => {
        const fileStorePrefix = filesApi.getfileStorePrefix(record.preview?.fileStoreSchema);
        return http.baseUri + fileStorePrefix + record.preview?.relativePath;
    }

    return (
        <img style={style} className="record-image" src={getFilePath()} alt="preview" height={height} width={width}/>
    );
}