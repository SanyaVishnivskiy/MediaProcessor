import React from "react";
import { IRecord } from "../../../entities/records/models";
import { RecordDownloadButton } from "./record-download-button";

interface RecordActionsBlockProp {
    record: IRecord
}

export const RecordActionsBlock = ({record}: RecordActionsBlockProp) => {
    return (
        <div>
            <RecordDownloadButton record={record}/>
        </div>
    );
}