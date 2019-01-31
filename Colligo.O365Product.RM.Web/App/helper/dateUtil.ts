export class DateUtil {
    /**
    * get the email sent time
    * @param sentDate
    */
    static getSentTime(sentDate: Date) {

        let dateCreated = new Date(sentDate);
        let dateToFormat = (dateCreated.getMonth() + 1).toString() + '/' + dateCreated.getDate() + '/' + dateCreated.getFullYear();
        let formatDate = this.getFormattedDate(dateToFormat);
        let dformat = formatDate + ' ' +
            [dateCreated.getHours(),
            dateCreated.getMinutes(),
            dateCreated.getSeconds()].join('.');

        return dformat;
    }

    static getCurrentDate() {
        let stamp = Date.now();
        let dateCreated = new Date(stamp);
        return dateCreated;
    }

    static getCurrentDateString() {
        let stamp = Date.now();
        let dateCreated = new Date(stamp);
        let month = dateCreated.getMonth() + 1;
        let year = dateCreated.getFullYear();
        let date = dateCreated.getDate();
        let monthStr = (month < 10) ? "0" + month.toString() : month.toString();
        let dateStr = (date < 10) ? "0" + date.toString() : date.toString();
        return year.toString() + '-' + monthStr + '-' + dateStr;
    }

    static getFirstDateOfCurrentDateString() {
        let stamp = Date.now();
        let dateCreated = new Date(stamp);
        let month = dateCreated.getMonth() + 1;
        let year = dateCreated.getFullYear();
        let monthStr = (month < 10) ? "0" + month.toString() : month.toString();
        return year.toString() + '-' + monthStr + '-01';
    }

    static getCurrentUTCDate() {
        let stamp = Date.now();
        let dateCreated = new Date(new Date(stamp).getUTCDate());
        return dateCreated;
    }

    /**
     * get the formatted datetime
     * @param input
     */
    static getFormattedDate(input: string) {
        var pattern = /(.*?)\/(.*?)\/(.*?)$/;
        var result = input.replace(pattern, function (match, p1, p2, p3) {
            var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
            return (p2 < 10 ? "0" + p2 : p2) + months[(p1 - 1)] + p3;
        });
        return result;
    }
    constructor() { }

}