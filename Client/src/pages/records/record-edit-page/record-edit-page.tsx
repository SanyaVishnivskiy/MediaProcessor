import React, { CSSProperties, useEffect, useState } from "react";
import { RouteComponentProps } from "react-router-dom";
import { RecordActionsBlock } from "../../../components/records/actions/record-actions-block";
import { RecordFieldsBlock } from "../../../components/records/Details/record-fields-block";
import { RecordImageBlock } from "../../../components/records/main/record-image-block";
import { IRecord } from "../../../entities/records/models";
import { RecordsService } from "../../../services/records/records-service";
import "./record-edit-page.css"
import history from "../../../entities/search/history";
import { Button, Col, Container, Row } from "react-bootstrap";

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
        if (!record) {
            history.push(`/`);
        }
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

    if (!record) {
        return (<h2>Fetching...</h2>);
    }
    
    return (
        <div style={{marginBottom: '20px'}}>
            <h1 className="d-flex justify-content-center">Record</h1>
            <Container>
                <Row xs="1" md="2">
                    <Col>
                        <Row>
                            <Col>
                                <RecordImageBlock 
                                    style={{marginBottom: '20px', width: '100%'}}
                                    record={record}/>
                            </Col>
                        </Row>
                        <Row>
                            <Col>
                                <RecordActionsBlock record={record} />
                            </Col>
                        </Row>
                    </Col>
                    <Col>
                        <RecordFieldsBlock record={record} onChange={onChange}/>
                        <div className="d-flex justify-content-center">
                            <Button style={{marginTop: '10px'}} size="lg" onClick={saveRecord}>Save</Button>
                        </div>
                    </Col>
                </Row>
            </Container>
        </div>
    );
}

