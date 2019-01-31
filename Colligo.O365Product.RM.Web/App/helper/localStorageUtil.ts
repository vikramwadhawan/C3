export class LocalStorageUtil {

    public static setValue(obj: any, key: string) {
        if (obj && key) {
            let stringifyObj = JSON.stringify(obj);
            localStorage.setItem(key, stringifyObj);
        }
    }

    public static getValue(key: string) {
        if (key) {
            return localStorage.getItem(key);
        }
    }

    public static removeItem(key: string) {
        if (key) {
            localStorage.removeItem(key);
        }
    }

    public static clearLocalStorage() {
        localStorage.clear();
    }
}