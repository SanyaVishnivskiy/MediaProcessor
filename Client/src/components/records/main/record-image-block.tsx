import { IRecord } from "../../../entities/records/models";
import * as http from "../../../services/api/http"
import * as filesApi from "../../../services/api/files-urls"

interface RecordImageBlock {
    record: IRecord,
    height?: string,
    width?: string
}

export const RecordImageBlock = ({record, height, width}: RecordImageBlock) => {
    const getFilePath = () => {
        const fileStorePrefix = filesApi.getfileStorePrefix(record.preview?.fileStoreSchema);
        return http.baseUri + fileStorePrefix + record.preview?.relativePath;
    }

    return (
        <div className="record-image-container">
            <img className="record-image" src={getFilePath()} alt="preview" height={height} width={width}/>
        </div>
    );
}