System.register(["./service.base", "@angular/core", "../app.constant", "@angular/http"], function (exports_1, context_1) {
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
    var service_base_1, core_1, app_constant_1, http_1, EventSettingService;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (service_base_1_1) {
                service_base_1 = service_base_1_1;
            },
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (app_constant_1_1) {
                app_constant_1 = app_constant_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            }
        ],
        execute: function () {
            EventSettingService = (function (_super) {
                __extends(EventSettingService, _super);
                function EventSettingService() {
                    return _super !== null && _super.apply(this, arguments) || this;
                }
                EventSettingService.prototype.saveEventSetup = function (context) {
                    if (context) {
                        var headers = new http_1.Headers();
                        headers.append('Content-Type', 'application/json');
                        return this._http.post(app_constant_1.CONFIGURATION.colligoRMOApp.saveEventSetup, { 'setup': JSON.stringify(context) }, { headers: headers })
                            .map(function (res) {
                            var data = res.json();
                            return data;
                        });
                    }
                };
                EventSettingService.prototype.updateEventSetup = function (context) {
                    if (context) {
                        var headers = new http_1.Headers();
                        headers.append('Content-Type', 'application/json');
                        return this._http.post(app_constant_1.CONFIGURATION.colligoRMOApp.updateEventSetup, { 'setup': JSON.stringify(context) }, { headers: headers })
                            .map(function (res) {
                            var data = res.json();
                            return data;
                        });
                    }
                };
                EventSettingService.prototype.loadAllEventSettingsByOrgUrl = function (url) {
                    if (url) {
                        var param = app_constant_1.CONFIGURATION.colligoRMOApp.getAllEventSetupByOrgUrl + "?url=" + encodeURIComponent(url);
                        return this._http.get(param, this.requestOptions)
                            .map(function (response) {
                            return JSON.parse(response.json());
                        });
                    }
                };
                EventSettingService.prototype.getEventSettingCount = function (url) {
                    if (url) {
                        var param = app_constant_1.CONFIGURATION.colligoRMOApp.getEventSettingCount + "?url=" + encodeURIComponent(url);
                        return this._http.get(param, this.requestOptions)
                            .map(function (response) {
                            return JSON.parse(response.json());
                        });
                    }
                };
                EventSettingService = __decorate([
                    core_1.Injectable()
                ], EventSettingService);
                return EventSettingService;
            }(service_base_1.ServiceBase));
            exports_1("EventSettingService", EventSettingService);
        }
    };
});
//# sourceMappingURL=eventsetting.service.js.map