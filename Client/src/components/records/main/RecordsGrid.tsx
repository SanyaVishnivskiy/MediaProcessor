import React from "react";
import { IRecord } from "../../../entities/records/models"
import { RecordBlock } from "./RecordBlock";


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
                    <div>
                        {records.map(x => 
                            <RecordBlock record={x}/>
                        )}
                    </div>          
                </div>)
            }
        </div>
    );
}