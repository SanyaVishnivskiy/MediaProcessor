import React, { ChangeEvent, ChangeEventHandler } from "react";
import { Form } from "react-bootstrap";
import { IRecord } from "../../../entities/records/models";
import { InputElement } from "../../common/inputs/input-element";

interface RecordFieldsBlockProps {
    record: IRecord;
    onChange: (record: IRecord) => void
}

export const RecordFieldsBlock = ({record, onChange}: RecordFieldsBlockProps) => {

    const onNameChanged = (value: string) => {
        const newRecord = { ...record, fileName: value };
        onChange(newRecord);
    }
    
    const onDescriptionChanged = (value: string) => {
        const newRecord = { ...record, description: value };
        onChange(newRecord);
    }

    return (
        <Form>
            <InputElement id={"recordId"} inputType="text" value={record.id} label={"Id:"} disabled onChange={() => {}}/>
            <InputElement id={"recordName"} inputType="text" value={record.fileName ?? ""} label={"Filename:"} onChange={onNameChanged}/>
            <InputElement id={"recordDescription"} inputType="text" value={record.description ?? ""} label={"Description:"} onChange={onDescriptionChanged}/>
        </Form>
    );
}