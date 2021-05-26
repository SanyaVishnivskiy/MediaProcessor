import { IRecord } from "../../../entities/records/models";
import { RecordsService } from "../../../services/records/records-service";
import history from "../../../entities/search/history";
import { Button } from "react-bootstrap";

interface RecordDeleteButtonProps {
    record: IRecord,
    className?: string,
    variant?: string,
    size?: "sm" | "lg"
}

export const RecordDeleteButton = (props: RecordDeleteButtonProps) => {
    const service = new RecordsService();

    const deleteRecord = async (): Promise<void> => {
        await service.delete(props.record.id);
        history.push(`/`);
    }
    
    return (
        <div>
            <Button block={true} size={props.size} className={props.className} variant={props.variant} onClick={deleteRecord}>Delete</Button>
        </div>
    )
}