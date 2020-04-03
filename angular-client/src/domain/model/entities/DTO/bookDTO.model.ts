export class BookDTO {
    constructor(
        public id: number = 0,
        public title: string = "",
        public authors: string = "",
        public year: number = 0,
        public language: string = "",
        public pageCount: number = 0,
        public description: string = "",
        public price: number = 0,
        public bookCover: string = "",
        public categoryID: number | null = null,
        public publisherID: number | null = null) { }
}
