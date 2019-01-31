System.register(["@angular/core", "@angular/platform-browser-dynamic", "./app.module", "./environments/environment"], function (exports_1, context_1) {
    "use strict";
    var core_1, platform_browser_dynamic_1, app_module_1, environment_1;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (platform_browser_dynamic_1_1) {
                platform_browser_dynamic_1 = platform_browser_dynamic_1_1;
            },
            function (app_module_1_1) {
                app_module_1 = app_module_1_1;
            },
            function (environment_1_1) {
                environment_1 = environment_1_1;
            }
        ],
        execute: function () {
            if (environment_1.environment.production) {
                core_1.enableProdMode();
            }
            setTimeout(function () {
                platform_browser_dynamic_1.platformBrowserDynamic().bootstrapModule(app_module_1.AppModule)
                    .catch(function (err) { return console.log(err); });
            }, 200);
        }
    };
});
//# sourceMappingURL=main.js.map