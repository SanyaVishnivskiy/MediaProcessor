import { ChangeEvent, ChangeEventHandler } from "react";
import { IRecord } from "../../../entities/records/models";

interface RecordFieldsBlockProps {
    record: IRecord;
    onChange: (record: IRecord) => void
}

export const RecordFieldsBlock = ({record, onChange}: RecordFieldsBlockProps) => {

    const onNameChanged = (e: ChangeEvent<HTMLInputElement>) => {
        const value = e.target.value;
        const newRecord = { ...record, fileName: value };
        onChange(newRecord);
    }

    return (
        <div>
            <form>
                <label htmlFor="recordId">Id:</label>
                <input id="recordId" value={record.id} disabled/>
                <br/>
                <label htmlFor="recordName">Filename:</label>
                <input id="recordName" value={record.fileName} onChange={onNameChanged}/>
            </form>
        </div>
    );
}