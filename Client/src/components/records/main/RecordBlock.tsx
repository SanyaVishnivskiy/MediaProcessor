import { Link } from "react-router-dom";
import { IRecord } from "../../../entities/records/models"
import { RecordImageBlock } from "./record-image-block"
import "./records.css"


interface RecordBlockProps {
    record: IRecord
}

export const RecordBlock = ({record}: RecordBlockProps) => {
    return (
        <Link className="record-block" to={"/records/" + record.id}>
            <div>
                <h5>Name: {record.fileName}</h5>
                <h5>Id: {record.id}</h5>
                <hr/>
                <h6>Preview:</h6>
                <RecordImageBlock record={record}/>
            </div>
        </Link>
    );
}