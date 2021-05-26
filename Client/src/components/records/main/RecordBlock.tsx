import React, { CSSProperties } from "react";
import Card from "react-bootstrap/Card";
import { IRecord } from "../../../entities/records/models"
import "./records.css"
import * as http from "../../../services/api/http"
import * as filesApi from "../../../services/api/files-urls"
import { Redirect } from "../../../services/navigation/redirect";


interface RecordBlockProps {
    record: IRecord
}

export const RecordBlock = ({record}: RecordBlockProps) => {
    const descriptionMaxLength = 50;
    
    const getFilePath = () => {
        const fileStorePrefix = filesApi.getfileStorePrefix(record.preview?.fileStoreSchema);
        return http.baseUri + fileStorePrefix + record.preview?.relativePath;
    }

    const shortDescription = () => {
        if (!record.description) {
            return "";
        }

        if (record.description.length <= descriptionMaxLength) {
            return record.description;
        }

        return record.description
            .substring(0, descriptionMaxLength - 3)
            + "...";
    }

    const redirect = () => {
        Redirect.toRecord(record.id);
    }

    const imageStyles : CSSProperties = {
        width: '100%',
        height: '15vw',
        objectFit: 'cover'
    }

    const cardStyles : CSSProperties = {
        minWidth: '300px',
        marginBottom: '30px'
    }

    const cardBodyStyles: CSSProperties = {
        height: '100px'
    }

    return (
        <Card onClick={redirect} style={cardStyles}>
            <Card.Img variant="top" src={getFilePath()} style={imageStyles}/>
            <Card.Body style={cardBodyStyles}>
                <Card.Title>{record.fileName}</Card.Title>
                <Card.Text>{shortDescription()}</Card.Text>
            </Card.Body>
        </Card>
    );
}