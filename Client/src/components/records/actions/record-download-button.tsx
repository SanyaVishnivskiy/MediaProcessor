import React from "react";
import { Button } from "react-bootstrap";
import { IRecord } from "../../../entities/records/models";
import { RecordsService } from "../../../services/records/records-service";

interface RecordDownloadButtonProps {
    record: IRecord,
    className?: string,
    variant?: string,
    size?: "sm" | "lg"
}

export const RecordDownloadButton = (props: RecordDownloadButtonProps) => {
    const service = new RecordsService();

    const download = async (): Promise<void> => {
        const link = service.getDownloadLink(props.record);
        window.open(link, "_blank");
    }
    
    return (
        <div>
            <Button size={props.size} className={props.className} variant={props.variant} onClick={download} block>Download</Button>
        </div>
    )
}