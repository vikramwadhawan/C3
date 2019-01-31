System.register(["./organization.component", "../../directive/equalvalidator.directive", "./../../app.routing", "@angular/forms", "@angular/http", "@angular/platform-browser", "ngx-select-ex", "@angular/core", "ngx-infinite-scroll", "../../services/eventsettinguser.service", "../shared/shared.module"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var organization_component_1, equalvalidator_directive_1, app_routing_1, forms_1, http_1, platform_browser_1, ngx_select_ex_1, core_1, ngx_infinite_scroll_1, eventsettinguser_service_1, shared_module_1, OrganizationModule;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (organization_component_1_1) {
                organization_component_1 = organization_component_1_1;
            },
            function (equalvalidator_directive_1_1) {
                equalvalidator_directive_1 = equalvalidator_directive_1_1;
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
            function (ngx_select_ex_1_1) {
                ngx_select_ex_1 = ngx_select_ex_1_1;
            },
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (ngx_infinite_scroll_1_1) {
                ngx_infinite_scroll_1 = ngx_infinite_scroll_1_1;
            },
            function (eventsettinguser_service_1_1) {
                eventsettinguser_service_1 = eventsettinguser_service_1_1;
            },
            function (shared_module_1_1) {
                shared_module_1 = shared_module_1_1;
            }
        ],
        execute: function () {
            OrganizationModule = (function () {
                function OrganizationModule() {
                }
                OrganizationModule = __decorate([
                    core_1.NgModule({
                        imports: [
                            platform_browser_1.BrowserModule,
                            http_1.HttpModule,
                            forms_1.FormsModule,
                            forms_1.ReactiveFormsModule,
                            app_routing_1.appRouting,
                            ngx_infinite_scroll_1.InfiniteScrollModule,
                            ngx_select_ex_1.NgxSelectModule,
                            shared_module_1.SharedModule
                        ],
                        exports: [
                            organization_component_1.OrganizationComponent
                        ],
                        declarations: [
                            organization_component_1.OrganizationComponent,
                            equalvalidator_directive_1.EqualValidatorDirective
                        ],
                        providers: [
                            eventsettinguser_service_1.EventSettingUserService
                        ],
                    })
                ], OrganizationModule);
                return OrganizationModule;
            }());
            exports_1("OrganizationModule", OrganizationModule);
        }
    };
});
//# sourceMappingURL=organization.module.js.map