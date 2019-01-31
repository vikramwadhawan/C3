System.register([], function (exports_1, context_1) {
    "use strict";
    var UniqueIdUtil;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [],
        execute: function () {
            UniqueIdUtil = (function () {
                function UniqueIdUtil() {
                }
                UniqueIdUtil.uniqueNumber = function () {
                    var date = Date.now();
                    if (date <= this.previous) {
                        date = ++this.previous;
                    }
                    else {
                        this.previous = date;
                    }
                    return date;
                };
                UniqueIdUtil.UID = function () {
                    return this.uniqueNumber();
                };
                UniqueIdUtil.previous = 0;
                return UniqueIdUtil;
            }());
            exports_1("UniqueIdUtil", UniqueIdUtil);
        }
    };
});
//# sourceMappingURL=UniqueIdUtil.js.map