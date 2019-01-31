export class UniqueIdUtil {
    private static uniqueNumber() {
        var date = Date.now();

        if (date <= this.previous) {
            date = ++this.previous;
        } else {
            this.previous = date;
        }
        return date;
    }

    private static previous: any = 0;

    public static UID() {
        return this.uniqueNumber();
    }
}