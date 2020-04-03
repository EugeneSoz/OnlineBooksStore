import { Publisher } from "./entities/publisher.model";
import { PublisherDTO } from "./entities/DTO/publisherDTO.model";
import { Category } from "./entities/category.model";
import { CategoryDTO } from "./entities/DTO/categoryDTO.model";
import { BookResponse } from "./entities/DTO/book-response.model";
import { BookDTO } from "./entities/DTO/bookDTO.model";

export class EntityExtensions {
    mapPublisherDTO(publisher: Publisher): PublisherDTO {
        return new PublisherDTO(publisher.id, publisher.name, publisher.country);
    }

    mapCategoryDTO(category: Category): CategoryDTO {
        return new CategoryDTO(category.id, category.name, category.parentCategoryID);
    }

    mapBookDTO(book: BookResponse): BookDTO {
        return new BookDTO(book.id, book.title, book.authors, book.year, book.language, book.pageCount,
            book.description, book.price, book.bookCover, book.categoryID, book.publisherID);
    }
}
