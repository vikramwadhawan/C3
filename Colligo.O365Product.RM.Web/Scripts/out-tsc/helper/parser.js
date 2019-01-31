System.register(["./localStorageUtil", "../model/sharepointRequest", "rxjs/add/operator/mergeMap", "../app.constant"], function (exports_1, context_1) {
    "use strict";
    var localStorageUtil_1, sharepointRequest_1, app_constant_1, Parser;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (localStorageUtil_1_1) {
                localStorageUtil_1 = localStorageUtil_1_1;
            },
            function (sharepointRequest_1_1) {
                sharepointRequest_1 = sharepointRequest_1_1;
            },
            function (_1) {
            },
            function (app_constant_1_1) {
                app_constant_1 = app_constant_1_1;
            }
        ],
        execute: function () {
            Parser = (function () {
                function Parser() {
                }
                Parser.getSharepointRequestObject = function () {
                    var detailJson = localStorage.getItem(app_constant_1.CONFIGURATION.localStorage.keys.Node_Detail);
                    var spRequest = null;
                    if (detailJson) {
                        var detailObj = JSON.parse(detailJson);
                        spRequest = new sharepointRequest_1.SharepointRequest();
                        spRequest.name = detailObj.name;
                        spRequest.serverRelativeUrl = detailObj.serverRelativeUrl;
                        spRequest.url = detailObj.url;
                        spRequest.type = detailObj.type;
                        spRequest.clientTimeZone = localStorageUtil_1.LocalStorageUtil.getValue(app_constant_1.CONFIGURATION.localStorage.keys.TIMEZONE);
                    }
                    return spRequest;
                };
                Parser.getTenantUrl = function () {
                    return localStorageUtil_1.LocalStorageUtil.getValue(app_constant_1.CONFIGURATION.localStorage.keys.Tenant_Url);
                };
                Parser.getCurrentUserId = function () {
                    var id = 0;
                    var data = localStorageUtil_1.LocalStorageUtil.getValue(app_constant_1.CONFIGURATION.localStorage.keys.CurrentLoginUserId);
                    if (data) {
                        id = parseInt(data);
                    }
                    return id;
                };
                Parser.getOrganizationMasterId = function () {
                    var id = 0;
                    var data = localStorageUtil_1.LocalStorageUtil.getValue(app_constant_1.CONFIGURATION.localStorage.keys.OrganizationId);
                    if (data) {
                        id = parseInt(data);
                    }
                    return id;
                };
                Parser.clearLocalStorageForModeChange = function () {
                    localStorageUtil_1.LocalStorageUtil.removeItem(app_constant_1.CONFIGURATION.localStorage.keys.Node_Detail);
                };
                Parser.isLogout = function () {
                    var logout = true;
                    var access_token = localStorage.getItem(app_constant_1.CONFIGURATION.localStorage.keys.AUTH_TOKEN);
                    if (access_token != undefined && access_token !== null && access_token !== '') {
                        logout = false;
                    }
                    return logout;
                };
                return Parser;
            }());
            exports_1("Parser", Parser);
        }
    };
});
//# sourceMappingURL=parser.js.map