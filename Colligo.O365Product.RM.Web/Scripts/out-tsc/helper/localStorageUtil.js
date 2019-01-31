System.register([], function (exports_1, context_1) {
    "use strict";
    var LocalStorageUtil;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [],
        execute: function () {
            LocalStorageUtil = (function () {
                function LocalStorageUtil() {
                }
                LocalStorageUtil.setValue = function (obj, key) {
                    if (obj && key) {
                        var stringifyObj = JSON.stringify(obj);
                        localStorage.setItem(key, stringifyObj);
                    }
                };
                LocalStorageUtil.getValue = function (key) {
                    if (key) {
                        return localStorage.getItem(key);
                    }
                };
                LocalStorageUtil.removeItem = function (key) {
                    if (key) {
                        localStorage.removeItem(key);
                    }
                };
                LocalStorageUtil.clearLocalStorage = function () {
                    localStorage.clear();
                };
                return LocalStorageUtil;
            }());
            exports_1("LocalStorageUtil", LocalStorageUtil);
        }
    };
});
//# sourceMappingURL=localStorageUtil.js.map