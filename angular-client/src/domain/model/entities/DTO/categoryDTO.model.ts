export class CategoryDTO {
    constructor(
        public id: number = 0,
        public name: string = "",
        public parentCategoryID: number | null = null) { }
}
