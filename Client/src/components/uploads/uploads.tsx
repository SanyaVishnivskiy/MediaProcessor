import React, { ChangeEventHandler, useState } from "react";
import axios from "axios";

export const FileUpload = () => {
    const [file, setFile] = useState(new File([],""));
    const [fileName, setFileName] = useState("");

    const saveFile = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.files == null)
            return;

        console.log(e.target.files?.item(0));
        setFile(e.target.files.item(0) ?? new File([],""));
        setFileName(e.target.files?.item(0)?.name ?? "")
    }

    const uploadFile = async () => {
        console.log(file);
        const formData = new FormData();
        formData.append("formFile", file);
        formData.append("fileName", fileName);
        try {
            const res = await axios.post("https://localhost:9300/files", formData);
            console.log(res);
        } catch (e) {
            console.log(e);
        }
    }

    return (
        <div>
            <input type="file" onChange={saveFile} />
            <input type="button" value="upload" onClick={uploadFile} />
        </div>
    )
}