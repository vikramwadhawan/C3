System.register(["@angular/core", "@angular/forms"], function (exports_1, context_1) {
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
    var core_1, forms_1, EqualValidatorDirective;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (forms_1_1) {
                forms_1 = forms_1_1;
            }
        ],
        execute: function () {
            EqualValidatorDirective = (function () {
                function EqualValidatorDirective() {
                }
                EqualValidatorDirective_1 = EqualValidatorDirective;
                EqualValidatorDirective.prototype.validate = function (control) {
                    var controlToCompare = control.parent.get(this.appConfirmEqualValidator);
                    if (controlToCompare && controlToCompare.value !== control.value) {
                        console.log('no');
                        return { 'notEqual': true };
                    }
                    return null;
                };
                var EqualValidatorDirective_1;
                __decorate([
                    core_1.Input(),
                    __metadata("design:type", String)
                ], EqualValidatorDirective.prototype, "appConfirmEqualValidator", void 0);
                EqualValidatorDirective = EqualValidatorDirective_1 = __decorate([
                    core_1.Directive({
                        selector: '[appConfirmEqualValidator]',
                        providers: [
                            {
                                provide: forms_1.NG_VALIDATORS,
                                useExisting: core_1.forwardRef(function () { return EqualValidatorDirective_1; }),
                                multi: true
                            }
                        ]
                    })
                ], EqualValidatorDirective);
                return EqualValidatorDirective;
            }());
            exports_1("EqualValidatorDirective", EqualValidatorDirective);
        }
    };
});
//# sourceMappingURL=equalvalidator.directive.js.map