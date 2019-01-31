System.register(["./service.base", "@angular/core", "../app.constant"], function (exports_1, context_1) {
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
    var service_base_1, core_1, app_constant_1, SharepointdataService;
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
            }
        ],
        execute: function () {
            SharepointdataService = (function (_super) {
                __extends(SharepointdataService, _super);
                function SharepointdataService() {
                    return _super !== null && _super.apply(this, arguments) || this;
                }
                SharepointdataService.prototype.loadSharePointSites = function (spRequest) {
                    var _this = this;
                    if (spRequest) {
                        return this.tryGetSPToken(spRequest).flatMap(function (data) {
                            if (data !== null) {
                                spRequest.accessToken = data;
                                spRequest.idToken = _this.token;
                                return _this._http.post(app_constant_1.CONFIGURATION.colligoRMOApp.searchSharepointSite, JSON.stringify(spRequest), _this.requestOptions)
                                    .map(function (res) {
                                    return JSON.parse(res.json());
                                });
                            }
                            else {
                                _this.ReturnError(app_constant_1.CONFIGURATION.constants.messages.SP_TOKEN_MISSING);
                            }
                        });
                    }
                    else {
                        return this.ReturnError(app_constant_1.CONFIGURATION.constants.messages.REQUEST_OBJECT_MISSING);
                    }
                };
                SharepointdataService.prototype.loadContentType = function (spRequest) {
                    var _this = this;
                    if (spRequest) {
                        return this.tryGetSPToken(spRequest).flatMap(function (data) {
                            if (data !== null) {
                                spRequest.accessToken = data;
                                spRequest.idToken = _this.token;
                                return _this._http.post(app_constant_1.CONFIGURATION.colligoRMOApp.getAllContentLog, JSON.stringify(spRequest), _this.requestOptions)
                                    .map(function (res) {
                                    return JSON.parse(res.json());
                                });
                            }
                            else {
                                _this.ReturnError(app_constant_1.CONFIGURATION.constants.messages.SP_TOKEN_MISSING);
                            }
                        });
                    }
                    else {
                        return this.ReturnError(app_constant_1.CONFIGURATION.constants.messages.REQUEST_OBJECT_MISSING);
                    }
                };
                SharepointdataService.prototype.loadMetadataFields = function (spRequest) {
                    var _this = this;
                    if (spRequest) {
                        return this.tryGetSPToken(spRequest).flatMap(function (data) {
                            if (data !== null) {
                                spRequest.accessToken = data;
                                spRequest.idToken = _this.token;
                                return _this._http.post(app_constant_1.CONFIGURATION.colligoRMOApp.getAllFieldsByContentId, JSON.stringify(spRequest), _this.requestOptions)
                                    .map(function (res) {
                                    return JSON.parse(res.json());
                                });
                            }
                            else {
                                _this.ReturnError(app_constant_1.CONFIGURATION.constants.messages.SP_TOKEN_MISSING);
                            }
                        });
                    }
                    else {
                        return this.ReturnError(app_constant_1.CONFIGURATION.constants.messages.REQUEST_OBJECT_MISSING);
                    }
                };
                SharepointdataService = __decorate([
                    core_1.Injectable()
                ], SharepointdataService);
                return SharepointdataService;
            }(service_base_1.ServiceBase));
            exports_1("SharepointdataService", SharepointdataService);
        }
    };
});
//# sourceMappingURL=sharepointdata.service.js.map