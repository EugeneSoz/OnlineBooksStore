export class Url {
    private static baseoptionsUrl: string = `/api/options`;
    static dataOptions: string = `${Url.baseoptionsUrl}/services`;
    static options_seed: string = `${Url.baseoptionsUrl}/seed`;
    static options_apply: string = `${Url.baseoptionsUrl}/apply`;
    static options_save: string = `${Url.baseoptionsUrl}/save`;
    static options_clear: string = `${Url.baseoptionsUrl}/clear`;
    static options_context: string = `${Url.baseoptionsUrl}/context`;

    private static baseCategoryUrl: string = `/api/category`;
    static storeCategories: string = `${Url.baseCategoryUrl}/storecategories`;
    static categories: string = `${Url.baseCategoryUrl}/categories`;
    static parentCategories: string = `${Url.baseCategoryUrl}/parentcategories`;
    static category: string = `${Url.baseCategoryUrl}/category`;
    static category_create: string = `${Url.baseCategoryUrl}/create`;
    static category_update: string = `${Url.baseCategoryUrl}/update`;
    static category_delete: string = `${Url.baseCategoryUrl}/delete`;
    static categoriesForSelection: string = `${Url.baseCategoryUrl}/categoriesforselection`;

    private static baseBookUrl: string = `/api/book`;
    static books: string = `${Url.baseBookUrl}/books`;
    static book: string = `${Url.baseBookUrl}/book`;
    static book_create: string = `${Url.baseBookUrl}/create`;
    static book_update: string = `${Url.baseBookUrl}/update`;
    static book_delete: string = `${Url.baseBookUrl}/delete`;

    private static basePublisherUrl: string = `/api/publisher`;
    static publishers: string = `${Url.basePublisherUrl}/publishers`;
    static publisher: string = `${Url.basePublisherUrl}/publisher`;
    static publisher_create: string = `${Url.basePublisherUrl}/create`;
    static publisher_update: string = `${Url.basePublisherUrl}/update`;
    static publisher_delete: string = `${Url.basePublisherUrl}/delete`;
    static publishersForSelection: string = `${Url.basePublisherUrl}/publishersforselection`;

    private static baseSessionUrl: string = `/api/session`;
    static cart_session: string = `${Url.baseSessionUrl}/cart`;
    static checkout_session: string = `${Url.baseSessionUrl}/checkout`;

    private static baseOrderUrl: string = `/api/orders`;
    static orders: string = `${Url.baseOrderUrl}`;

    private static baseAccountUrl: string = `/api/account`;
    static login: string = `${Url.baseAccountUrl}/login`;
    static logout: string = `${Url.baseAccountUrl}/logout`;

    private static basePropertyUrl: string = "api/properties";
    static pub_filter: string = `${Url.basePropertyUrl}/pub_filter`;
    static pub_sorting: string = `${Url.basePropertyUrl}/pub_sorting`;
    static cat_filter: string = `${Url.basePropertyUrl}/cat_filter`;
    static cat_sorting: string = `${Url.basePropertyUrl}/cat_sorting`;
    static book_filter: string = `${Url.basePropertyUrl}/book_filter`;
    static book_sorting: string = `${Url.basePropertyUrl}/book_sorting`;
    static dropdown: string = `${Url.basePropertyUrl}/dropdown`;
}
