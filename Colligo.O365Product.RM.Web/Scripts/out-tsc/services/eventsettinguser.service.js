System.register(["./service.base", "@angular/http", "@angular/core", "../app.constant"], function (exports_1, context_1) {
    "use strict";
    var __extends = (this && this.__extends) || (function () {
        var extendStatics = function (d, b) {
            extendStatics = Object.setPrototypeOf ||
                ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
                function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
            return extendStatics(d, b);
        }
        return function (d, b) {
            extendStatics(d, b);
            function __() { this.constructor = d; }
            d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
        };
    })();
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var service_base_1, http_1, core_1, app_constant_1, EventSettingUserService;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (service_base_1_1) {
                service_base_1 = service_base_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (app_constant_1_1) {
                app_constant_1 = app_constant_1_1;
            }
        ],
        execute: function () {
            EventSettingUserService = (function (_super) {
                __extends(EventSettingUserService, _super);
                function EventSettingUserService() {
                    return _super !== null && _super.apply(this, arguments) || this;
                }
                EventSettingUserService.prototype.saveEventSettingUser = function (data) {
                    if (data !== null) {
                        var headers = new http_1.Headers();
                        headers.append('Content-Type', 'application/json');
                        return this._http.post(app_constant_1.CONFIGURATION.colligoRMOApp.saveEventSettingUser, { 'data': JSON.stringify(data) }, { headers: headers })
                            .map(function (res) { return res.text(); });
                    }
                    else {
                        return this.ReturnError(app_constant_1.CONFIGURATION.constants.messages.REQUEST_OBJECT_MISSING);
                    }
                };
                EventSettingUserService.prototype.getEventSettingUser = function (siteUrl) {
                    if (siteUrl.length > 0) {
                        return this._http.get(app_constant_1.CONFIGURATION.colligoRMOApp.getEventSettingUser + "?siteUrl=" + encodeURI(siteUrl), this.requestOptions)
                            .map(function (res) {
                            return JSON.parse(res.json());
                        });
                    }
                    else {
                        return this.ReturnError(app_constant_1.CONFIGURATION.constants.messages.REQUEST_OBJECT_MISSING);
                    }
                };
                EventSettingUserService.prototype.getTimeZone = function () {
                    return this._http.get(app_constant_1.CONFIGURATION.colligoRMOApp.getTimeZone, this.requestOptions)
                        .map(function (res) {
                        return JSON.parse(res.json());
                    });
                };
                EventSettingUserService = __decorate([
                    core_1.Injectable()
                ], EventSettingUserService);
                return EventSettingUserService;
            }(service_base_1.ServiceBase));
            exports_1("EventSettingUserService", EventSettingUserService);
        }
    };
});
//# sourceMappingURL=eventsettinguser.service.js.map