import { IRecord } from "../../../entities/records/models";
import history from "../../../entities/search/history";

interface RecordActionsPageButtonProps {
    record: IRecord;
}

export const RecordActionsPageButton = ({record}: RecordActionsPageButtonProps) => {
    const redirect = () => {
        history.push(`/records/${record.id}/actions`);
    }

    return (
        <div>
            <button onClick={() => redirect()} >Actions</button>
        </div>
    );
}