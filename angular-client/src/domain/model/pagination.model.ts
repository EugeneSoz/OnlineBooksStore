export class Pagination {
    constructor(
        public currentPage?: number,
        public pageSize?: number,
        public totalPages?: number,
        public hasPreviousPage?: boolean,
        public hasNextPage?: boolean,
        public leftBoundary?: number,
        public rightBoundary?: number) { }
}
