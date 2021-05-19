import * as http from "../../services/api/http"
import { v4 as uuidv4 } from "uuid";

export class FilesService {
    uri = "files";
    chunkSize = 1048576 * 20; //its 20MB, 

    async save(fileName: string, file: File): Promise<void> {
        if (this.isBigFile(file)) {
            await this.saveBigFile(fileName, file);
            return;
        }

        await this.saveSmallFile(fileName, file);
    }

    private async saveSmallFile(fileName: string, file: File) {
        const formData = new FormData();
        formData.append("formFile", file);
        formData.append("fileName", fileName);

        const response = await http.post(this.uri, formData);
    }

    private isBigFile(file: File): boolean {
        return file.size > this.chunkSize;
    }

    private async saveBigFile(fileName: string, file: File) {
        const chunksCount = this.getChunksCount(file);
        const fileId = uuidv4();

        for (let i = 0; i < chunksCount; i++) {
            const chunk = this.sliceChunk(file, i);
            const response = await this.uploadChunk(chunk, fileId, i);
        }

        const completeResponse = await this.completeUpload(chunksCount, fileId, fileName);
    }

    private getChunksCount(file: File): number {
        return file.size % this.chunkSize == 0
            ? file.size / this.chunkSize
            : Math.floor(file.size / this. chunkSize) + 1;
    }

    private sliceChunk(file: File, i: number): Blob {
        return file.slice(
            i * this.chunkSize,
            (i * this.chunkSize) + this.chunkSize);
    }

    private async uploadChunk(chunk: Blob, fileId: string, i: number) {
        const formData = new FormData();
        formData.append("file", chunk);
        formData.append("fileId", fileId);
        formData.append("number", i.toString());

        const response = await http.post(this.uri + "/chunks", formData);
    }

    private async completeUpload(chunksCount: number, fileId: string, fileName: string) {
        const request = {
            fileId: fileId,
            chunksCount: chunksCount,
            fileName: fileName
        };

        const response = await http.post(this.uri + "/chunks/complete", request);
    }
}
