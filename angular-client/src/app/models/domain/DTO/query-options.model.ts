export class QueryOptions {
    constructor(
        public currentPage?: number,
        public pageSize?: number,
        public sortPropertyName?: string,
        public descendingOrder?: boolean,
        public searchPropertyNames?: Array<string>,
        public searchTerm?: string,
        public filterPropertyName?: string,
        public filterPropertyValue?: number) { }

        resetToDefault()
        {
            this.currentPage = 1;
            this.pageSize = 12;
            this.sortPropertyName = "";
            this.descendingOrder = false;
            this.searchPropertyNames = null;
            this.searchTerm = "";
            this.filterPropertyName = "";
            this.filterPropertyValue = 0;
        }
}
