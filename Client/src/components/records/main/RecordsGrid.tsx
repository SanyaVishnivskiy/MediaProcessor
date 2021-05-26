import React from "react";
import { Col, Container, Row } from "react-bootstrap";
import { IRecord } from "../../../entities/records/models"
import { RecordBlock } from "./RecordBlock";
import "./records.css"

interface RecordsGridProps {
    records: IRecord[]
}

export const RecordsGrid = ({records}: RecordsGridProps) => {
    const chunksPerRow = 3;
    
    const getBatches = () : IRecord[][] => {
        let result: IRecord[][] = [];
        for (let i = 0; i < records.length; i += chunksPerRow) {
            result.push(records.slice(i , i + chunksPerRow));
        }
        console.log(records, result);
        return result;
    }

    if (getBatches().length === 0) {
        return <h3 className="d-flex justify-content-center">No records found</h3>;
    }

    return (
        <div>
            {
            getBatches().map((x, i) =>
                <Container>
                    <Row xs="1" sm="2" md="3">
                        {x.map((r, j) => 
                            <Col >
                                <RecordBlock record={r} key={i + j}/>
                            </Col>)}
                    </Row>
                </Container>
            )}
        </div>
    );
}