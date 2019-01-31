System.register(["./message-banner.component", "@angular/forms", "@angular/platform-browser", "./loader.component", "./required-asterik.component", "@angular/core", "./lightbox.component"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var message_banner_component_1, forms_1, platform_browser_1, loader_component_1, required_asterik_component_1, core_1, lightbox_component_1, SharedModule;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (message_banner_component_1_1) {
                message_banner_component_1 = message_banner_component_1_1;
            },
            function (forms_1_1) {
                forms_1 = forms_1_1;
            },
            function (platform_browser_1_1) {
                platform_browser_1 = platform_browser_1_1;
            },
            function (loader_component_1_1) {
                loader_component_1 = loader_component_1_1;
            },
            function (required_asterik_component_1_1) {
                required_asterik_component_1 = required_asterik_component_1_1;
            },
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (lightbox_component_1_1) {
                lightbox_component_1 = lightbox_component_1_1;
            }
        ],
        execute: function () {
            SharedModule = (function () {
                function SharedModule() {
                }
                SharedModule = __decorate([
                    core_1.NgModule({
                        imports: [
                            platform_browser_1.BrowserModule,
                            forms_1.FormsModule
                        ],
                        exports: [
                            lightbox_component_1.LightBoxComponent,
                            loader_component_1.LoaderComponent,
                            required_asterik_component_1.RequiredAsterikComponent,
                            message_banner_component_1.MessageBannerComponent
                        ],
                        declarations: [
                            lightbox_component_1.LightBoxComponent,
                            loader_component_1.LoaderComponent,
                            required_asterik_component_1.RequiredAsterikComponent,
                            message_banner_component_1.MessageBannerComponent,
                        ],
                        providers: [],
                    })
                ], SharedModule);
                return SharedModule;
            }());
            exports_1("SharedModule", SharedModule);
        }
    };
});
//# sourceMappingURL=shared.module.js.map