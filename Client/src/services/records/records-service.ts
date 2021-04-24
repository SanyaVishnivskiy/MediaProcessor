import * as http from "../../services/api/http"
import { IRecord } from "../../entities/records/models";

export class RecordsService {
    uri = "records";

    async get(): Promise<IRecord[]> {
        const response = await http.get(this.uri);
        if (response.data == null)
            return new Array<IRecord>();

        return response.data as IRecord[];
    }

    async getById(id: string): Promise<IRecord> {
        const response = await http.get(this.uri + "/" + id);
        return response.data as IRecord;
    }

    async save(record: IRecord): Promise<void> {
        const response = await http.put(this.uri + "/" + record.id, record);
    }
}