import { IPagination } from "../../../entities/search/models"
import React from "react"
import "./pagination.css"
import ReactPaginate from "react-paginate";

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
        const newPagination = formNewPagination(selected);
        props.onPageChanged(newPagination);
    }

    const totalPages = (): number => {
        return Math.round(props.totalItems / props.pagination.size);
    }

    const pages = (): number[] => {
        let pages: number[] = [];
        for (let i = 1; i <= totalPages(); i++) {
            pages.push(i);
        }

        return pages;
    }
    
    return (
        <div>
            <ul className="pagination">
                {pages().map(page => {
                    return (
                    <li key={page} onClick={() => onPageChanged(page)}>
                        {page}
                    </li>
                    );
                })}
            </ul>
        </div>
    );
}