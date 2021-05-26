import React, { useEffect, useState } from "react"
import { RouteComponentProps } from "react-router-dom";
import { PaginationComponent } from "../../../components/common/pagination/pagination-component";
import { RecordsGrid } from "../../../components/records/main/RecordsGrid";
import { IRecord } from "../../../entities/records/models"
import { IPagination, IRecordSearchContext, SearchResult, urlParamsToPagination } from "../../../entities/search/models";
import { objToQuery, queryToObj } from "../../../services/common/query-converter";
import { RecordsService } from "../../../services/records/records-service";
import history from "../../../entities/search/history";
import { Container, Row, Col, Button, Form } from "react-bootstrap";

const getInitialPagination = (): IRecordSearchContext => {
    return {
        size: 6,
        page: 1,
        sortBy: 'id',
        sortOrder: 'desc',
        search: ''
    };
}

interface IRecordPageProps extends RouteComponentProps {
}

export const RecordsPage = (props: IRecordPageProps) => {
    const service = new RecordsService();
    const [records, setRecords] = useState(new SearchResult<IRecord>());
    const [searchContext, setSearchContext] = useState<IRecordSearchContext>(getInitialPagination());
    const [search, setSearch] = useState<string>('');

    const fetchRecords = async (context: IRecordSearchContext) : Promise<void> => {
        const result = await service.get(context);
        setRecords(result);
    }

    const onPageChange = async (pagination: IPagination) : Promise<void> => {
        const newContext : IRecordSearchContext = { ...searchContext, page: +pagination.page}
        setSearchContext(newContext);
        history.push("?" + objToQuery(newContext));
    }

    const onSearchChange = (search: string) => {
        setSearch(search);
    }

    const searchRecords = async (): Promise<void> => {
        const context = {...getInitialPagination(), search: search};
        setSearchContext(context);
        history.push("?" + objToQuery(context));
    }

    const getPagination = () => {
        let pagination = urlParamsToPagination(queryToObj(props.location.search));
        pagination = pagination ? pagination : getInitialPagination();
        return pagination;
    }

    useEffect(() => {
        const pagination = getPagination();
        fetchRecords(pagination);
      }, [searchContext.page, searchContext.size, searchContext.sortBy, searchContext.sortOrder, searchContext.search]);

    return (
        <div>
            <h1 className="d-flex justify-content-center">Records</h1>
            <Container>
                <Row>
                    <Col sm="10">
                        <Form.Group controlId={"search"} as={Row}>
                            <Form.Control
                                type={"text"}
                                name={"search"} 
                                onChange={e => onSearchChange(e.target.value)}
                                value={search} />
                        </Form.Group>
                    </Col>
                    <Col sm="2">
                        <Button variant="primary" block onClick={() => searchRecords()}>
                            Search
                        </Button>
                    </Col>
                </Row>
            </Container>
            <RecordsGrid records={records.items}/>
            <PaginationComponent 
                totalItems={records.totalItems}
                pagination={searchContext as IPagination}
                onPageChanged={onPageChange}
            />
        </div>
    );
}