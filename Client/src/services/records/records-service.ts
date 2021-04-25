import * as http from "../../services/api/http"
import { IRecord } from "../../entities/records/models";
import { saveAs } from 'file-saver';

export class RecordsService {
    uri = "records";

    async get(): Promise<IRecord[]> {
        const response = await http.get(this.uri);
        if (response.data == null)
            return new Array<IRecord>();

        return response.data as IRecord[];
    }

    async getById(id: string): Promise<IRecord> {
        const response = await http.get(this.uriWithId(id));
        return response.data as IRecord;
    }

    async save(record: IRecord): Promise<void> {
        const response = await http.put(this.uriWithId(record.id), record);
    }

    async download(record: IRecord): Promise<void> {
        const response = await http.download(this.uriWithId(record.id) + "/download");
        saveAs(response, record.fileName);
    }

    private uriWithId(id: string): string{
        return this.uri + "/" + id;
    }
}