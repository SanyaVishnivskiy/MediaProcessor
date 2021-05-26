export interface IPagination {
    size: number;
    page: number;
    sortBy: string;
    sortOrder: 'asc' | 'desc';
}

export interface IRecordSearchContext extends IPagination {
    search: string;
}

export const urlParamsToPagination = (params: URLSearchParams): IRecordSearchContext | null => {
    const size = params.get('size');
    const page = params.get('page');
    const sortBy = params.get('sortBy');
    const sortOrder = params.get('sortOrder'); 
    const search = params.get('search'); 

    if (!size || !page || !sortBy || !sortOrder) {
        return null;
    }

    return {
        size: size ? +size : 0,
        page: page ? +page : 0,
        sortBy: sortBy ?? '',
        sortOrder: sortOrder === 'desc' ? 'desc' : 'asc',
        search: search ?? ''
    }
} 

export class SearchResult<T> {
    size: number = 0;
    page: number = 0;
    totalItems: number = 0;
    items: T[] = [];

}