export class Url {
    private static baseoptionsUrl = `/api/options`;
    static dataOptions = `${Url.baseoptionsUrl}/services`;
    static options_seed = `${Url.baseoptionsUrl}/seed`;
    static options_apply = `${Url.baseoptionsUrl}/apply`;
    static options_save = `${Url.baseoptionsUrl}/save`;
    static options_clear = `${Url.baseoptionsUrl}/clear`;
    static options_context = `${Url.baseoptionsUrl}/context`;

    private static baseStoreUrl = `/api/store`;
    static storeBooks = `${Url.baseStoreUrl}/books`;
    static storeBook = `${Url.baseStoreUrl}/book`;
    static storeCategories = `${Url.baseStoreUrl}/categories`;

    private static baseCategoryUrl = `/api/category`;
    static categories = `${Url.baseCategoryUrl}/categories`;
    static parentCategories = `${Url.baseCategoryUrl}/parentcategories`;
    static category = `${Url.baseCategoryUrl}/category`;
    static category_create = `${Url.baseCategoryUrl}/create`;
    static category_update = `${Url.baseCategoryUrl}/update`;
    static category_delete = `${Url.baseCategoryUrl}/delete`;
    static categoriesForSelection = `${Url.baseCategoryUrl}/categoriesforselection`;

    private static baseBookUrl = `/api/book`;
    static books = `${Url.baseBookUrl}/books`;
    static book = `${Url.baseBookUrl}/book`;
    static book_create = `${Url.baseBookUrl}/create`;
    static book_update = `${Url.baseBookUrl}/update`;
    static book_delete = `${Url.baseBookUrl}/delete`;

    private static basePublisherUrl = `/api/publisher`;
    static publishers = `${Url.basePublisherUrl}/publishers`;
    static publisher = `${Url.basePublisherUrl}/publisher`;
    static publisher_create = `${Url.basePublisherUrl}/create`;
    static publisher_update = `${Url.basePublisherUrl}/update`;
    static publisher_delete = `${Url.basePublisherUrl}/delete`;
    static publishersForSelection = `${Url.basePublisherUrl}/publishersforselection`;

    private static baseSessionUrl = `/api/session`;
    static cart_session = `${Url.baseSessionUrl}/cart`;
    static checkout_session = `${Url.baseSessionUrl}/checkout`;

    private static baseOrderUrl = `/api/orders`;
    static orders = `${Url.baseOrderUrl}`;

    private static baseAccountUrl = `/api/account`;
    static login = `${Url.baseAccountUrl}/login`;
    static logout = `${Url.baseAccountUrl}/logout`;

    private static basePropertyUrl = "api/properties";
    static pub_filter = `${Url.basePropertyUrl}/pub_filter`;
    static pub_sorting = `${Url.basePropertyUrl}/pub_sorting`;
    static cat_filter = `${Url.basePropertyUrl}/cat_filter`;
    static cat_sorting = `${Url.basePropertyUrl}/cat_sorting`;
    static book_filter = `${Url.basePropertyUrl}/book_filter`;
    static book_sorting = `${Url.basePropertyUrl}/book_sorting`;
    static dropdown = `${Url.basePropertyUrl}/dropdown`;
}
