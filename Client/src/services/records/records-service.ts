import * as http from "../../services/api/http"
import { IRecord } from "../../entities/records/models";
import { saveAs } from 'file-saver';
import { objToQuery, queryToObj } from "../common/query-converter";
import { IPagination, SearchResult } from "../../entities/search/models";

export class RecordsService {
    uri = "records";
    
    async get(pagination: IPagination): Promise<SearchResult<IRecord>> {
        const queryString = objToQuery(pagination);
        const response = await http.get(this.uri + "?" + queryString);
        if (response.data == null)
            return new SearchResult<IRecord>();

        return response.data as SearchResult<IRecord>;
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