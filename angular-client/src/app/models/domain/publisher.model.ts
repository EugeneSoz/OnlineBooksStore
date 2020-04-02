import { PublisherDTO } from './DTO/publisherDTO.model';
import { Book } from './book.model';

export class Publisher extends PublisherDTO {
    constructor(
        public books: Array<Book> = null) {

        super();
    }
}
