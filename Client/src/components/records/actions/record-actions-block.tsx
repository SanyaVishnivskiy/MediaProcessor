import React from "react";
import { Col, Container, Row } from "react-bootstrap";
import { IRecord } from "../../../entities/records/models";
import { RecordDeleteButton } from "../Details/record-delete-button";
import { RecordActionsPageButton } from "./record-actions-page-button";
import { RecordDownloadButton } from "./record-download-button";

interface RecordActionsBlockProp {
    record: IRecord
}

export const RecordActionsBlock = ({record}: RecordActionsBlockProp) => {

    return (
        <Container>
            <Row xs="1" md="3">
                <Col>
                    <RecordDownloadButton size="lg" className="" record={record}/>
                </Col>
                <Col>
                    <RecordActionsPageButton size="lg" className= "" record={record}/>
                </Col>
                <Col>
                    <RecordDeleteButton size="lg" className="" variant="danger" record={record}/>
                </Col>
            </Row>
        </Container>
    );
}