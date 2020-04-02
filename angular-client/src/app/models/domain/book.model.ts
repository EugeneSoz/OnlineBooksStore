import { BookDTO } from './DTO/bookDTO.model';
import { Category } from './category.model';
import { Publisher } from './publisher.model';

export class Book extends BookDTO {
    constructor(
        public category: Category = null,
        public publisher: Publisher = null) {

        super();
    }
}
