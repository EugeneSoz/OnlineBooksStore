import { CategoryDTO } from './categoryDTO.model';

export class StoreCategoryResponse extends CategoryDTO {
    constructor(
        public controlId: string = "",
        public isParent: boolean = false,
        public hasChildren: boolean = false,
        public isCollapsed: boolean = true,
        public children: Array<StoreCategoryResponse> = null) {

        super();
    }
}
