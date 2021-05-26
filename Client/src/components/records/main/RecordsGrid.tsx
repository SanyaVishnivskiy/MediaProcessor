import React from "react";
import { CardColumns, CardDeck } from "react-bootstrap";
import { IRecord } from "../../../entities/records/models"
import { RecordBlock } from "./RecordBlock";
import "./records.css"

interface RecordsGridProps {
    records: IRecord[]
}

export const RecordsGrid = ({records}: RecordsGridProps) => {
    return (
        <CardColumns>
            {!records || records.length === 0
                ? (<h3 className="d-flex justify-content-center">No records found</h3>)
                : records.map((x, i) => 
                        <RecordBlock record={x} key={i}/>
                    )
            }
        </CardColumns>
    );
}