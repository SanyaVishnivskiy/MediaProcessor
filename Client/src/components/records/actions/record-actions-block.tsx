import React from "react";
import { IRecord } from "../../../entities/records/models";
import { RecordActionsPageButton } from "./record-actions-page-button";
import { RecordDownloadButton } from "./record-download-button";

interface RecordActionsBlockProp {
    record: IRecord
}

export const RecordActionsBlock = ({record}: RecordActionsBlockProp) => {
    return (
        <div>
            <RecordDownloadButton record={record}/>
            <RecordActionsPageButton record={record}/>
        </div>
    );
}