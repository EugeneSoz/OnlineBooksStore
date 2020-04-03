declare global {
    interface StringConstructor {
        isNullOrEmpty(value: string): boolean;
        empty: string;
    }
}

export {}
