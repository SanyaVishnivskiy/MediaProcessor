import React, { CSSProperties, useState } from "react";
import { FilesService } from "../../services/files/files-service";
import history from "../../entities/search/history";
import { Button } from "react-bootstrap";

export const FileUpload = () => {
    const service = new FilesService();

    const [file, setFile] = useState(new File([],""));
    const [fileName, setFileName] = useState("");

    const saveFile = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files == null)
            return;

        setFile(e.target.files.item(0) ?? new File([],""));
        setFileName(e.target.files?.item(0)?.name ?? "")
    }

    const uploadFile = async () => {
        await service.save(fileName, file);
        history.push(`/`);
    }

    return (
        <div style={{marginTop: '30px'}}>
            <div className="d-flex justify-content-center">
                <input type="file" onChange={saveFile} />
                <Button onClick={uploadFile} variant="primary">Upload</Button>
            </div>
        </div>
    )
}