import { Pagination } from './pagination.model';

export class PagedResponse<T>{
    constructor(
        public entities?: Array<T>,
        public pagination?: Pagination,
        public pageNumbers?: Array<number>) {
    }
}
