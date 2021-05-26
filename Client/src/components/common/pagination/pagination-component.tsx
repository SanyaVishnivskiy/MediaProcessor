import { IPagination } from "../../../entities/search/models"
import React, { CSSProperties } from "react"
import { Pagination } from "react-bootstrap";

interface PaginationComponentProps {
    pagination: IPagination;
    totalItems: number;
    onPageChanged: (pagination: IPagination) => Promise<void>;
}

export const PaginationComponent = (props: PaginationComponentProps) => {
    const formNewPagination = (page: number): IPagination => {
        return {
            ...props.pagination,
            page: page
        };
    };

    const onPageChanged = (selected: number) => {
        if (!selected)
            return;

        if (selected == props.pagination.page)
            return;

        const newPagination = formNewPagination(selected);
        props.onPageChanged(newPagination);
    }

    const totalPages = (): number => {
        return Math.ceil(props.totalItems / props.pagination.size);
    }

    const pages = (): any[] => {
        let pages: any[] = [];
        for (let i = 1; i <= totalPages(); i++) {
            pages.push(
              <Pagination.Item key={i} active={i === +props.pagination.page}>
                {i}
              </Pagination.Item>);
        }

        return pages;
    }

    const paginationContainerStyles: CSSProperties = {
        width: '100%'
    };
    
    if (totalPages() <= 1) {
        return <></>
    }

    return (
        <div style={paginationContainerStyles}>
            <Pagination
                className="justify-content-center"
                onClick={(e: any) => onPageChanged(e.target.text)}>
                    {pages()}
            </Pagination>
        </div>
    );
}