System.register(["@angular/core"], function (exports_1, context_1) {
    "use strict";
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var core_1, WindowRef;
    var __moduleName = context_1 && context_1.id;
    function _window() {
        return window;
    }
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            }
        ],
        execute: function () {
            WindowRef = (function () {
                function WindowRef() {
                }
                Object.defineProperty(WindowRef.prototype, "nativeWindow", {
                    get: function () {
                        return _window();
                    },
                    enumerable: true,
                    configurable: true
                });
                WindowRef = __decorate([
                    core_1.Injectable()
                ], WindowRef);
                return WindowRef;
            }());
            exports_1("WindowRef", WindowRef);
        }
    };
});
//# sourceMappingURL=windowRef.service.js.map