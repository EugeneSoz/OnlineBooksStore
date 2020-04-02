import { CategoryDTO } from './categoryDTO.model';

export class CategoryResponse extends CategoryDTO {
    constructor(
        public parentCategoryName?: string,
        public displayedName?: string) {

        super();
    }
}
