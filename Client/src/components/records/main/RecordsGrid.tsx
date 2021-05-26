import React from "react";
import { CardDeck } from "react-bootstrap";
import { IRecord } from "../../../entities/records/models"
import { RecordBlock } from "./RecordBlock";
import "./records.css"

interface RecordsGridProps {
    records: IRecord[]
}

export const RecordsGrid = ({records}: RecordsGridProps) => {
    return (
        <CardDeck>
            {!records || records.length === 0
                ? (<h3 className="d-flex justify-content-center">No records found</h3>)
                : records.map((x, i) => 
                        <RecordBlock record={x} key={i}/>
                    )
            }
        </CardDeck>
    );
}