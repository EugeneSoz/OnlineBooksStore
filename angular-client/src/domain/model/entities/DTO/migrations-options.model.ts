export class MigrationsOptions {
    constructor(
        public booksCount: number = 0,
        public categoriesCount: number = 0,
        public publishersCount: number = 0,
        public contextNames: Array<string> = new Array<string>(),
        public appliedMigrations: Array<string> = new Array<string>(),
        public pendingMigrations: Array<string> = new Array<string>(),
        public allMigrations: Array<string> = new Array<string>(),
        public infoMessage: string = "") {
    }
}
