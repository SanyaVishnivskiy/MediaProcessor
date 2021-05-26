import * as http from "../../services/api/http"
import { IRecord } from "../../entities/records/models";
import { objToQuery } from "../common/query-converter";
import { IRecordSearchContext, SearchResult } from "../../entities/search/models";
import { IAction } from "../../entities/actions/models";

export class RecordsService {
    uri = "records";

    async get(context: IRecordSearchContext): Promise<SearchResult<IRecord>> {
        const queryString = objToQuery(context);
        const response = await http.get(this.uri + "?" + queryString);
        if (response.data == null)
            return new SearchResult<IRecord>();

        return response.data as SearchResult<IRecord>;
    }

    async getById(id: string): Promise<IRecord | null> {
        try {
            const response = await http.get(this.uriWithId(id));
            return response.data as IRecord;
        } catch (e){
            return null;
        } 
    }

    async save(record: IRecord): Promise<void> {
        const response = await http.put(this.uriWithId(record.id), record);
    }

    getDownloadLink(record: IRecord): string {
        return http.getDownloadLink(this.uriWithId(record.id) + "/download");
    }

    async delete(recordId: string): Promise<void> {
        const response = await http.httpDelete(this.uriWithId(recordId));
    }

    async getActions(id: string): Promise<IAction[]> {
        const response = await http.get(this.uriWithId(id) + "/actions")
        if (!response.data) {
            return new Array<IAction>();
        }
        
        return response.data as IAction[];
    }

    private uriWithId(id: string): string{
        return this.uri + "/" + id;
    }
}