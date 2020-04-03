//класс содержит элементы списка в панели Toolbar
export class ListItem {
    constructor(
        public propertyName: string,
        public name: string,
        public descendingOrder: boolean = false,
        public hasDivider: boolean = false,
        public href: string = "#") { }
}

export class Dropdown {
    constructor(
        public sortingProperties: Array<ListItem>,
        public gridSizeProperties: Array<ListItem>) { }
}
