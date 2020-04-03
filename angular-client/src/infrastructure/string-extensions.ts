////string-extensions.ts

interface StringConstructor {
    isNullOrEmpty(value: string): boolean;
    empty: string;
}

String.empty = "";
String.isNullOrEmpty = value => !(typeof value === "string" && value.length > 0);
