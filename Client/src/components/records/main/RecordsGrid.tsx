import React from "react";
import { IRecord } from "../../../entities/records/models"
import { RecordBlock } from "./RecordBlock";
import "./records.css"

interface RecordsGridProps {
    records: IRecord[]
}

export const RecordsGrid = ({records}: RecordsGridProps) => {
    return (
        <div>
            {!records || records.length === 0
                ? (<h3>No records found</h3>)
                : (
                <div>
                    <div className="records-container">
                        {records.map((x, i) => 
                            <RecordBlock record={x} key={i}/>
                        )}
                    </div>          
                </div>)
            }
        </div>
    );
}