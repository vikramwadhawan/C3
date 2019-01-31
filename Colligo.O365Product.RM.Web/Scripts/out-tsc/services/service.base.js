System.register(["@angular/http", "rxjs/Observable", "../app.constant", "rxjs/add/operator/map", "rxjs/add/observable/throw", "@angular/core", "../helper/parser"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var http_1, Observable_1, app_constant_1, http_2, core_1, parser_1, ServiceBase;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (http_1_1) {
                http_1 = http_1_1;
                http_2 = http_1_1;
            },
            function (Observable_1_1) {
                Observable_1 = Observable_1_1;
            },
            function (app_constant_1_1) {
                app_constant_1 = app_constant_1_1;
            },
            function (_1) {
            },
            function (_2) {
            },
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (parser_1_1) {
                parser_1 = parser_1_1;
            }
        ],
        execute: function () {
            ServiceBase = (function () {
                function ServiceBase(_http) {
                    this._http = _http;
                    this.setHeaderOptions();
                    this.tenentUrl = localStorage.getItem(app_constant_1.CONFIGURATION.localStorage.keys.Tenant_Url);
                }
                ServiceBase.prototype.tryGetToken = function () {
                    var token = this.defaultToken;
                    var access_token = localStorage.getItem(app_constant_1.CONFIGURATION.localStorage.keys.AUTH_TOKEN);
                    if (access_token) {
                        token = access_token;
                    }
                    return token;
                };
                ServiceBase.prototype.setHeaderOptions = function () {
                    this.token = this.tryGetToken();
                    this.requestOptions = { headers: new http_1.Headers() };
                    this.requestOptions.headers.set('Authorization', "Bearer " + this.token);
                    this.requestOptions.headers.set('Content-Type', "application/json; charset=utf-8");
                };
                ServiceBase.prototype.tryGetSPToken = function (spRequest) {
                    return this.tryGetSPTokenByUrl(spRequest.url);
                };
                ServiceBase.prototype.tryGetSPTokenByUrl = function (url) {
                    if (!parser_1.Parser.isLogout()) {
                        var token = null;
                        if (url) {
                            var headers = new http_1.Headers();
                            headers.append('Content-Type', 'application/json');
                            var requestUrl = app_constant_1.CONFIGURATION.colligoRMOApp.getSharePointRefreshToken + '?url=' + encodeURIComponent(url);
                            return this._http.get(requestUrl, { headers: headers })
                                .map(function (res) {
                                var data = res.json();
                                return data.data;
                            });
                        }
                    }
                    else {
                        window.location.href = '/Login/Logout';
                    }
                };
                ServiceBase.prototype.ReturnError = function (errorMessage) {
                    console.error(errorMessage);
                    return Observable_1.Observable.throw(errorMessage);
                };
                ServiceBase = __decorate([
                    core_1.Injectable(),
                    __metadata("design:paramtypes", [http_2.Http])
                ], ServiceBase);
                return ServiceBase;
            }());
            exports_1("ServiceBase", ServiceBase);
        }
    };
});
//# sourceMappingURL=service.base.js.map