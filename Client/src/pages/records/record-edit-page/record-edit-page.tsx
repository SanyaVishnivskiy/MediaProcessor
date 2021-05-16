import React, { useEffect, useState } from "react";
import { RouteComponentProps } from "react-router-dom";
import { RecordActionsBlock } from "../../../components/records/actions/record-actions-block";
import { RecordFieldsBlock } from "../../../components/records/Details/record-fields-block";
import { RecordImageBlock } from "../../../components/records/main/record-image-block";
import { IRecord } from "../../../entities/records/models";
import { RecordsService } from "../../../services/records/records-service";

interface RecordEditPageRouteParams {
    id: string
}

interface RecordEditPageProps extends RouteComponentProps<RecordEditPageRouteParams> {
}

export const RecordEditPage = (props: RecordEditPageProps) => {
    const service = new RecordsService();
    const id = props.match.params.id;
    
    const [record, setRecord] = useState<IRecord | null>(null);

    const fetchRecord = async () : Promise<void> => {
        const record = await service.getById(id);
        setRecord(record);
    }

    useEffect(() => {
        fetchRecord();
      }, []);

    const onChange = (record: IRecord) => {
        setRecord(record);
    }

    const saveRecord = async () => {
        if (record === null)
            return;
        await service.save(record);
    }
    
    return (
        <div>
            { record === null 
                ? (<h2>Fetching...</h2>)
                : (<div>
                    <div>Record:</div>
                    <RecordImageBlock record={record} height="200"/>
                    <RecordActionsBlock record={record} />
                    <RecordFieldsBlock record={record} onChange={onChange}/>
                    <button onClick={saveRecord}>Save</button>
                </div>)
            }
        </div>
    );
}

