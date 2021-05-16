import { IRecord } from "../../../entities/records/models";
import { RecordsService } from "../../../services/records/records-service";
import history from "../../../entities/search/history";

interface RecordDeleteButtonProps {
    record: IRecord
}

export const RecordDeleteButton = (props: RecordDeleteButtonProps) => {
    const service = new RecordsService();

    const deleteRecord = async (): Promise<void> => {
        await service.delete(props.record.id);
        history.push(`/`);
    }
    
    return (
        <div>
            <button onClick={deleteRecord}>Delete</button>
        </div>
    )
}