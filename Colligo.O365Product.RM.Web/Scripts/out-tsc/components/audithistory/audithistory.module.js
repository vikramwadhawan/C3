System.register(["./audithistory.component", "./../../app.routing", "@angular/forms", "@angular/http", "@angular/platform-browser", "@angular/core", "../../services/dashboard.service", "../shared/shared.module", "ngx-infinite-scroll"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var audithistory_component_1, app_routing_1, forms_1, http_1, platform_browser_1, core_1, dashboard_service_1, shared_module_1, ngx_infinite_scroll_1, AuditHistoryModule;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (audithistory_component_1_1) {
                audithistory_component_1 = audithistory_component_1_1;
            },
            function (app_routing_1_1) {
                app_routing_1 = app_routing_1_1;
            },
            function (forms_1_1) {
                forms_1 = forms_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (platform_browser_1_1) {
                platform_browser_1 = platform_browser_1_1;
            },
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (dashboard_service_1_1) {
                dashboard_service_1 = dashboard_service_1_1;
            },
            function (shared_module_1_1) {
                shared_module_1 = shared_module_1_1;
            },
            function (ngx_infinite_scroll_1_1) {
                ngx_infinite_scroll_1 = ngx_infinite_scroll_1_1;
            }
        ],
        execute: function () {
            AuditHistoryModule = (function () {
                function AuditHistoryModule() {
                }
                AuditHistoryModule = __decorate([
                    core_1.NgModule({
                        imports: [
                            platform_browser_1.BrowserModule,
                            http_1.HttpModule,
                            app_routing_1.appRouting,
                            shared_module_1.SharedModule,
                            ngx_infinite_scroll_1.InfiniteScrollModule,
                            forms_1.FormsModule
                        ],
                        exports: [
                            audithistory_component_1.AuditHistoryComponent
                        ],
                        declarations: [
                            audithistory_component_1.AuditHistoryComponent
                        ],
                        providers: [
                            dashboard_service_1.DashboardService
                        ],
                    })
                ], AuditHistoryModule);
                return AuditHistoryModule;
            }());
            exports_1("AuditHistoryModule", AuditHistoryModule);
        }
    };
});
//# sourceMappingURL=audithistory.module.js.map