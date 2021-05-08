import { useEffect, useState } from "react";
import { RouteComponentProps } from "react-router-dom";
import { IAction, IRunActionsRequest } from "../../entities/actions/models";
import { JobsService } from "../../services/actions/jobs-service";

interface ActionsPageParams {
    id: string; 
}

interface ActionsPageProps extends RouteComponentProps<ActionsPageParams>  {
}

export const ActionsPage = (props: ActionsPageProps) => {
    const recordId = props.match.params.id;

    const jobsService = new JobsService();
    const [actions, setActions] = useState(new Array<IAction>());

    const fetchActions = () => {

    }

    const runActions = async () => {
        const request: IRunActionsRequest = {
            actions: actions,
            recordId: recordId
        };
        await jobsService.run(request);
    }

    useEffect(() =>{
        fetchActions();
    }, [])

    return (
        <div>
            <h1>Actions:</h1>
            <button onClick={() => runActions()}>Run</button>
        </div>
    );
}