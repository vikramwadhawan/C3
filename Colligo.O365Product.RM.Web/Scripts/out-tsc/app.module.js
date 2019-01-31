System.register(["@angular/core", "@angular/common", "@angular/platform-browser", "@angular/http", "./components/organization/organization.module", "./components/eventsetup/eventsetup.module", "./components/dashboard/dashboard.module", "./components/audithistory/audithistory.module", "./components/contentlog/contentlog.module", "./app.component", "./app.routing", "./services/windowRef.service", "./components/shared/shared.module"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var core_1, common_1, platform_browser_1, http_1, organization_module_1, eventsetup_module_1, dashboard_module_1, audithistory_module_1, contentlog_module_1, app_component_1, app_routing_1, windowRef_service_1, shared_module_1, AppModule;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (common_1_1) {
                common_1 = common_1_1;
            },
            function (platform_browser_1_1) {
                platform_browser_1 = platform_browser_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            },
            function (organization_module_1_1) {
                organization_module_1 = organization_module_1_1;
            },
            function (eventsetup_module_1_1) {
                eventsetup_module_1 = eventsetup_module_1_1;
            },
            function (dashboard_module_1_1) {
                dashboard_module_1 = dashboard_module_1_1;
            },
            function (audithistory_module_1_1) {
                audithistory_module_1 = audithistory_module_1_1;
            },
            function (contentlog_module_1_1) {
                contentlog_module_1 = contentlog_module_1_1;
            },
            function (app_component_1_1) {
                app_component_1 = app_component_1_1;
            },
            function (app_routing_1_1) {
                app_routing_1 = app_routing_1_1;
            },
            function (windowRef_service_1_1) {
                windowRef_service_1 = windowRef_service_1_1;
            },
            function (shared_module_1_1) {
                shared_module_1 = shared_module_1_1;
            }
        ],
        execute: function () {
            AppModule = (function () {
                function AppModule() {
                }
                AppModule = __decorate([
                    core_1.NgModule({
                        imports: [
                            platform_browser_1.BrowserModule,
                            http_1.HttpModule,
                            app_routing_1.appRouting,
                            eventsetup_module_1.EventSetupModule,
                            organization_module_1.OrganizationModule,
                            dashboard_module_1.DashboardModule,
                            audithistory_module_1.AuditHistoryModule,
                            contentlog_module_1.ContentLogModule,
                            shared_module_1.SharedModule
                        ],
                        declarations: [
                            app_component_1.AppComponent
                        ],
                        providers: [
                            { provide: common_1.APP_BASE_HREF, useValue: '/' },
                            windowRef_service_1.WindowRef
                        ],
                        bootstrap: [app_component_1.AppComponent]
                    })
                ], AppModule);
                return AppModule;
            }());
            exports_1("AppModule", AppModule);
        }
    };
});
//# sourceMappingURL=app.module.js.map