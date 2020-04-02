import { CategoryDTO } from './DTO/categoryDTO.model';
import { Book } from './book.model';

export class Category extends CategoryDTO {
    constructor(
        public displayedName?: string,
        public parentCategory?: Category,
        public childrenCategories?: Array<Category>,
        public books?: Array<Book>) {

        super();
    }
}
