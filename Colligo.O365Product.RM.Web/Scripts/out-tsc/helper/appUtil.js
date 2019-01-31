System.register([], function (exports_1, context_1) {
    "use strict";
    var AppUtil;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [],
        execute: function () {
            AppUtil = (function () {
                function AppUtil() {
                }
                AppUtil.RedirectTokenExpire = function () {
                    window.location.href = '/Login/Index';
                };
                return AppUtil;
            }());
            exports_1("AppUtil", AppUtil);
        }
    };
});
//# sourceMappingURL=appUtil.js.map