import * as http from "../../services/api/http"
import { IRecord } from "../../entities/records/models";

export class FilesService {
    uri = "files";

    async save(formData: FormData): Promise<void> {
        const response = await http.post(this.uri, formData);
    }
}