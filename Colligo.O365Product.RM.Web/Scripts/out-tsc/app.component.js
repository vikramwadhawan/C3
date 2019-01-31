System.register(["@angular/core", "@angular/router", "./app.constant", "./model/contentNode", "./helper/parser", "./helper/localStorageUtil"], function (exports_1, context_1) {
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
    var core_1, router_1, app_constant_1, contentNode_1, parser_1, localStorageUtil_1, AppComponent;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (app_constant_1_1) {
                app_constant_1 = app_constant_1_1;
            },
            function (contentNode_1_1) {
                contentNode_1 = contentNode_1_1;
            },
            function (parser_1_1) {
                parser_1 = parser_1_1;
            },
            function (localStorageUtil_1_1) {
                localStorageUtil_1 = localStorageUtil_1_1;
            }
        ],
        execute: function () {
            AppComponent = (function () {
                function AppComponent(router) {
                    var _this = this;
                    this.router = router;
                    this.showMenu = true;
                    this.router.routeReuseStrategy.shouldReuseRoute = function () {
                        return false;
                    };
                    this.router.events.subscribe(function (event) {
                        if (event instanceof router_1.NavigationEnd) {
                            _this.router.navigated = false;
                            window.scrollTo(0, 0);
                        }
                    });
                    this._router = router;
                    this.setContentNode();
                    console.log('rmo app triggered');
                }
                AppComponent.prototype.setContentNode = function () {
                    var node = new contentNode_1.ContentNode();
                    node.name = '';
                    node.serverRelativeUrl = '/';
                    node.url = parser_1.Parser.getTenantUrl();
                    localStorageUtil_1.LocalStorageUtil.setValue(node, app_constant_1.CONFIGURATION.localStorage.keys.Node_Detail);
                };
                AppComponent.prototype.onDisconnect = function () {
                    localStorage.removeItem(app_constant_1.CONFIGURATION.localStorage.keys.AUTH_TOKEN);
                    window.location.href = '/Login/Logout';
                };
                AppComponent.prototype.changeMenu = function () {
                    this.showMenu = !this.showMenu;
                };
                AppComponent = __decorate([
                    core_1.Component({
                        selector: 'rmo-app',
                        templateUrl: './app.component.html'
                    }),
                    __metadata("design:paramtypes", [router_1.Router])
                ], AppComponent);
                return AppComponent;
            }());
            exports_1("AppComponent", AppComponent);
        }
    };
});
//# sourceMappingURL=app.component.js.map