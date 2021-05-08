import * as http from "../api/http"
import { IRecord } from "../../entities/records/models";
import { IRunActionsRequest } from "../../entities/actions/models";
import { IJob, IServerJob, JobType } from "../../entities/jobs/models";

export class JobsService {
    uri = "jobs";

    async run(request: IRunActionsRequest): Promise<void> {
        const body = this.createBody(request);
        const response = await http.post(this.uri, body);
    }

    private createBody(request: IRunActionsRequest) : IServerJob {
        return {
            type: JobType[JobType.Actions],
            data: request
        }
    }
}