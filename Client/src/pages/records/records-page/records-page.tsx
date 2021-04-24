import React, { useEffect, useState } from "react"
import { RecordsGrid } from "../../../components/records/main/RecordsGrid";
import { IRecord } from "../../../entities/records/models"
import * as http from "../../../services/api/http"
import { RecordsService } from "../../../services/records/records-service";

export const RecordsPage = () => {
    const service = new RecordsService();
    const [records, setRecords] = useState(new Array<IRecord>());

    const refreshRecords = async () : Promise<void> => {
        const records = await service.get();
        setRecords(records);
    }

    useEffect(() => {
        refreshRecords();
        console.log("effect")
      }, []);

    return (
        <div>
            <h1>Records:</h1>
            <RecordsGrid records={records}/>
        </div>
    );
}