import { BookDTO } from './bookDTO.model';

export class BookResponse extends BookDTO {
    constructor(
        public categoryName?: string,
        public subcategoryName?: string,
        public publisherName ?: string) {

        super();
    }
}
