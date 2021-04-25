import { IRecord } from "../../../entities/records/models";
import { RecordsService } from "../../../services/records/records-service";

interface RecordDownloadButtonProps {
    record: IRecord
}

export const RecordDownloadButton = (props: RecordDownloadButtonProps) => {
    const service = new RecordsService();

    const download = async (): Promise<void> => {
        await service.download(props.record);
    }
    
    return (
        <div>
            <button onClick={download}>Download</button>
        </div>
    )
}