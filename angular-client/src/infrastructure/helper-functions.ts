export function nameof<T>(key: keyof T): keyof T {
    return key;
}

export function createPageLink(forwardSlash: boolean, ...routeSegements: string[]): string {
    let pageLink: string = "";
    if (forwardSlash) {
        pageLink = "/";
    }

    for (let i = 0; i < routeSegements.length; i++) {
        if (i < routeSegements.length - 1) {
            pageLink = pageLink + routeSegements[i] + "/";
        }
        else {
            pageLink = pageLink + routeSegements[i];
        }
    }

    return pageLink;
}
