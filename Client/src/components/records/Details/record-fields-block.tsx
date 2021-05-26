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

    const appendUTCifNotNull = (value: string | null): string | null => {
        if (!value)
            return null;

        return value + " UTC";
    }

    return (
        <Form>
            <InputElement id={"recordId"} inputType="text" value={record.id} label={"Id:"} disabled onChange={() => {}}/>
            <InputElement id={"recordName"} inputType="text" value={record.fileName ?? ""} label={"Filename:"} onChange={onNameChanged}/>
            <InputElement id={"recordDescription"} inputType="text" as="textarea" textAreaRows={3} value={record.description ?? ""} label={"Description:"} onChange={onDescriptionChanged}/>
            <InputElement id={"createdOn"} inputType="text" value={appendUTCifNotNull(record.createdOn) ?? ""} label={"Created on:"} onChange={() => {}} disabled/>
            <InputElement id={"createdBy"} inputType="text" value={record.createdBy ?? ""} label={"Created by:"} onChange={() => {}} disabled/>
            <InputElement id={"modifiedOn"} inputType="text" value={appendUTCifNotNull(record.modifiedOn) ?? ""} label={"Modified on:"} onChange={() => {}} disabled/>
            <InputElement id={"modifiedBy"} inputType="text" value={record.modifiedBy ?? ""} label={"Modified by:"} onChange={() => {}} disabled/>
        </Form>
    );
}