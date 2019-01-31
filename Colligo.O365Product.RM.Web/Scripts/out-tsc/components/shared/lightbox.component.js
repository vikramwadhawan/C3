System.register(["@angular/core"], function (exports_1, context_1) {
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
    var core_1, LightBoxComponent;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            }
        ],
        execute: function () {
            LightBoxComponent = (function () {
                function LightBoxComponent() {
                }
                LightBoxComponent.prototype.ngOnInit = function () { };
                __decorate([
                    core_1.Input('loading'),
                    __metadata("design:type", Boolean)
                ], LightBoxComponent.prototype, "_loading", void 0);
                __decorate([
                    core_1.Input('loaderMessage'),
                    __metadata("design:type", String)
                ], LightBoxComponent.prototype, "_loaderMessage", void 0);
                LightBoxComponent = __decorate([
                    core_1.Component({
                        selector: 'lightbox',
                        templateUrl: './lightbox.component.html',
                        styles: ["\n    .button\n{\n                width: 150px;\n                padding: 10px;\n                background-color: #FF8C00;\n                box-shadow: -8px 8px 10px 3px rgba(0,0,0,0.2);\n    font-weight:bold;\n                text-decoration:none;\n}\n.cover{\n    position:fixed;\n    top:0;\n    left:0;\n    background:rgba(0,0,0,0.13);\n    z-index:999;\n    width:100%;\n    height:100%;\n    display:block;\n}\n#loginScreen\n{\n    height:380px;\n    width:340px;\n    margin:0 auto;\n    position:relative;\n    z-index:10;\n    display:none;\n                background: url(login.png) no-repeat;\n                border:5px solid #cccccc;\n                border-radius:10px;\n}\n\n.cancel\n{\n    display:block;\n    position:absolute;\n    top:3px;\n    right:2px;\n    background:rgb(245,245,245);\n    color:black;\n    height:30px;\n    width:35px;\n    font-size:30px;\n    text-decoration:none;\n    text-align:center;\n    font-weight:bold;\n}\n#loginScreen:target, #loginScreen:target + #cover{\n    display:block;\n    opacity:2;\n}\n    "]
                    }),
                    __metadata("design:paramtypes", [])
                ], LightBoxComponent);
                return LightBoxComponent;
            }());
            exports_1("LightBoxComponent", LightBoxComponent);
        }
    };
});
//# sourceMappingURL=lightbox.component.js.map