System.register([], function (exports_1, context_1) {
    "use strict";
    var DateUtil;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [],
        execute: function () {
            DateUtil = (function () {
                function DateUtil() {
                }
                DateUtil.getSentTime = function (sentDate) {
                    var dateCreated = new Date(sentDate);
                    var dateToFormat = (dateCreated.getMonth() + 1).toString() + '/' + dateCreated.getDate() + '/' + dateCreated.getFullYear();
                    var formatDate = this.getFormattedDate(dateToFormat);
                    var dformat = formatDate + ' ' +
                        [dateCreated.getHours(),
                            dateCreated.getMinutes(),
                            dateCreated.getSeconds()].join('.');
                    return dformat;
                };
                DateUtil.getCurrentDate = function () {
                    var stamp = Date.now();
                    var dateCreated = new Date(stamp);
                    return dateCreated;
                };
                DateUtil.getCurrentDateString = function () {
                    var stamp = Date.now();
                    var dateCreated = new Date(stamp);
                    var month = dateCreated.getMonth() + 1;
                    var year = dateCreated.getFullYear();
                    var date = dateCreated.getDate();
                    var monthStr = (month < 10) ? "0" + month.toString() : month.toString();
                    var dateStr = (date < 10) ? "0" + date.toString() : date.toString();
                    return year.toString() + '-' + monthStr + '-' + dateStr;
                };
                DateUtil.getFirstDateOfCurrentDateString = function () {
                    var stamp = Date.now();
                    var dateCreated = new Date(stamp);
                    var month = dateCreated.getMonth() + 1;
                    var year = dateCreated.getFullYear();
                    var monthStr = (month < 10) ? "0" + month.toString() : month.toString();
                    return year.toString() + '-' + monthStr + '-01';
                };
                DateUtil.getCurrentUTCDate = function () {
                    var stamp = Date.now();
                    var dateCreated = new Date(new Date(stamp).getUTCDate());
                    return dateCreated;
                };
                DateUtil.getFormattedDate = function (input) {
                    var pattern = /(.*?)\/(.*?)\/(.*?)$/;
                    var result = input.replace(pattern, function (match, p1, p2, p3) {
                        var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
                        return (p2 < 10 ? "0" + p2 : p2) + months[(p1 - 1)] + p3;
                    });
                    return result;
                };
                return DateUtil;
            }());
            exports_1("DateUtil", DateUtil);
        }
    };
});
//# sourceMappingURL=dateUtil.js.map