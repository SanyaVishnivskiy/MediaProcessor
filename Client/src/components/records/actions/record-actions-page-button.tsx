import React from "react";
import { Button } from "react-bootstrap";
import { IRecord } from "../../../entities/records/models";
import history from "../../../entities/search/history";

interface RecordActionsPageButtonProps {
    record: IRecord;
    className?: string;
    variant?: string;
    size?: "sm" | "lg";
}

export const RecordActionsPageButton = ({record, className, variant, size}: RecordActionsPageButtonProps) => {
    const redirect = () => {
        history.push(`/records/${record.id}/actions`);
    }

    return (
        <div>
            <Button block={true} size={size} className={className} variant={variant} onClick={() => redirect()} >Actions</Button>
        </div>
    );
}