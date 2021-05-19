import React, { useEffect, useState } from "react";
import { RouteComponentProps } from "react-router-dom";
import { ActionsGrid } from "../../components/records/actions/grid/actions-grid";
import { IAction, IRunActionsRequest } from "../../entities/actions/models";
import { IRecord } from "../../entities/records/models";
import { JobsService } from "../../services/actions/jobs-service";
import { RecordsService } from "../../services/records/records-service";
import history from "../../entities/search/history";

interface ActionsPageParams {
    id: string; 
}

interface ActionsPageProps extends RouteComponentProps<ActionsPageParams>  {
}

export const ActionsPage = (props: ActionsPageProps) => {
    const recordId = props.match.params.id;

    const jobsService = new JobsService();
    const recordsService = new RecordsService();
    const [record, setRecord] = useState<IRecord | null>(null);
    const [possibleActions, setPossibleActions] = useState(new Array<IAction>());
    const [selectedActions, setSelectedActions] = useState(new Array<IAction>());

    const fetchRecord = async () => {
        const record = await recordsService.getById(recordId);
        setRecord(record);
    }
    
    const fetchActions = async () => {
        var actions = await recordsService.getActions(recordId);
        setPossibleActions(actions);
    }

    const runActions = async () => {
        const request: IRunActionsRequest = {
            actions: getSelectedActions(),
            recordId: recordId
        };
        await jobsService.run(request);
        history.push(`/records/${recordId}`)
    }

    const getSelectedActions = () => {
        selectedActions.forEach(a => {
            a.inputPath = record?.fileName ?? ''
        });

        return selectedActions;
    }

    const onSelectedActionsChange = (selectedActions: IAction[]) => {
        setSelectedActions([...selectedActions]);
    }

    useEffect(() =>{
        fetchRecord();
        fetchActions();
    }, [])

    return (
        <div>
            <h1>Actions:</h1>

            <ActionsGrid
                possibleActions={possibleActions}
                selectedActions={selectedActions}
                onSelectedActionsChange={onSelectedActionsChange}/>

            <button onClick={() => runActions()}>Run</button>
        </div>
    );
}