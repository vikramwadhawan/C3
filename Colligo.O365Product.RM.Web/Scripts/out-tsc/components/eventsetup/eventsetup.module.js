System.register(["./eventsetup.component", "./../../app.routing", "@angular/forms", "@angular/http", "@angular/platform-browser", "@angular/core", "ngx-infinite-scroll", "ngx-select-ex", "../../services/sharepointdata.service", "../../services/eventsetting.service", "../shared/shared.module"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var eventsetup_component_1, app_routing_1, forms_1, http_1, platform_browser_1, core_1, ngx_infinite_scroll_1, ngx_select_ex_1, sharepointdata_service_1, eventsetting_service_1, shared_module_1, EventSetupModule;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (eventsetup_component_1_1) {
                eventsetup_component_1 = eventsetup_component_1_1;
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
            function (ngx_infinite_scroll_1_1) {
                ngx_infinite_scroll_1 = ngx_infinite_scroll_1_1;
            },
            function (ngx_select_ex_1_1) {
                ngx_select_ex_1 = ngx_select_ex_1_1;
            },
            function (sharepointdata_service_1_1) {
                sharepointdata_service_1 = sharepointdata_service_1_1;
            },
            function (eventsetting_service_1_1) {
                eventsetting_service_1 = eventsetting_service_1_1;
            },
            function (shared_module_1_1) {
                shared_module_1 = shared_module_1_1;
            }
        ],
        execute: function () {
            EventSetupModule = (function () {
                function EventSetupModule() {
                }
                EventSetupModule = __decorate([
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
                            eventsetup_component_1.EventSetupComponent
                        ],
                        declarations: [
                            eventsetup_component_1.EventSetupComponent
                        ],
                        providers: [
                            sharepointdata_service_1.SharepointdataService,
                            eventsetting_service_1.EventSettingService
                        ],
                    })
                ], EventSetupModule);
                return EventSetupModule;
            }());
            exports_1("EventSetupModule", EventSetupModule);
        }
    };
});
//# sourceMappingURL=eventsetup.module.js.map