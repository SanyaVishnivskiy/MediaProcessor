import React, { useEffect, useState } from "react"
import { RouteComponentProps } from "react-router-dom";
import { PaginationComponent } from "../../../components/common/pagination/pagination-component";
import { RecordsGrid } from "../../../components/records/main/RecordsGrid";
import { IRecord } from "../../../entities/records/models"
import { IPagination, SearchResult, urlParamsToPagination } from "../../../entities/search/models";
import { objToQuery, queryToObj } from "../../../services/common/query-converter";
import { RecordsService } from "../../../services/records/records-service";
import history from "../../../entities/search/history";

const getInitialPagination = (): IPagination => {
    return {
        size: 2,
        page: 1,
        sortBy: 'id',
        sortOrder: 'desc'
    };
}

interface IRecordPageProps extends RouteComponentProps {
}

export const RecordsPage = (props: IRecordPageProps) => {
    const service = new RecordsService();
    const [records, setRecords] = useState(new SearchResult<IRecord>());
    const [pagination, setPagination] = useState<IPagination>(getInitialPagination());

    const fetchRecords = async (pagination: IPagination) : Promise<void> => {
        const result = await service.get(pagination);
        setRecords(result);
    }

    const onPageChange = async (pagination: IPagination) : Promise<void> => {
        setPagination(pagination);
        history.push("?" + objToQuery(pagination));
    }

    const getPagination = () => {
        let pagination = urlParamsToPagination(queryToObj(props.location.search));
        pagination = pagination ? pagination : getInitialPagination();
        return pagination;
    }

    useEffect(() => {
        const pagination = getPagination();
        fetchRecords(pagination);
      }, [pagination.page, pagination.size, pagination.sortBy, pagination.sortOrder]);

    return (
        <div>
            <h1>Records:</h1>
            <RecordsGrid records={records.items}/>
            <PaginationComponent 
                totalItems={records.totalItems}
                pagination={pagination}
                onPageChanged={onPageChange}
            />
        </div>
    );
}