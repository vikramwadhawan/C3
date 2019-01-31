System.register(["@angular/router", "./../../app.constant", "./../../model/eventSettingUserSetup/EventSettingUser", "./../../model/eventSettingUserSetup/agentTimeZoneModel", "./../../helper/parser", "@angular/core", "../../services/windowRef.service", "../../services/eventsettinguser.service", "@angular/forms"], function (exports_1, context_1) {
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
    var router_1, app_constant_1, EventSettingUser_1, agentTimeZoneModel_1, parser_1, core_1, windowRef_service_1, eventsettinguser_service_1, forms_1, OrganizationComponent;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (app_constant_1_1) {
                app_constant_1 = app_constant_1_1;
            },
            function (EventSettingUser_1_1) {
                EventSettingUser_1 = EventSettingUser_1_1;
            },
            function (agentTimeZoneModel_1_1) {
                agentTimeZoneModel_1 = agentTimeZoneModel_1_1;
            },
            function (parser_1_1) {
                parser_1 = parser_1_1;
            },
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (windowRef_service_1_1) {
                windowRef_service_1 = windowRef_service_1_1;
            },
            function (eventsettinguser_service_1_1) {
                eventsettinguser_service_1 = eventsettinguser_service_1_1;
            },
            function (forms_1_1) {
                forms_1 = forms_1_1;
            }
        ],
        execute: function () {
            OrganizationComponent = (function () {
                function OrganizationComponent(_wind, _router, element, _renderer, _eventSettingService, formBuilder) {
                    this._wind = _wind;
                    this._router = _router;
                    this.element = element;
                    this._renderer = _renderer;
                    this._eventSettingService = _eventSettingService;
                    this.formBuilder = formBuilder;
                    this.showAdgSection = false;
                    this.submitted = false;
                    this.siteTenantUrl = parser_1.Parser.getTenantUrl();
                    this.isProcessing = false;
                    this.showMessageBanner = false;
                    this.messageBannerType = '';
                    this.bannerMessage = '';
                    this.showEventUser = true;
                    this.availableTimeZones = [];
                    this.timeZones = [];
                    this.availableTimeHours = [];
                    this.availableTimeMinutes = [];
                    this.formControlClass = { 'class': 'form-control' };
                }
                OrganizationComponent.prototype.ngOnInit = function () {
                    this.loadUserForm();
                    this.loadTimeZoneForm();
                };
                OrganizationComponent.prototype.loadUserForm = function () {
                    try {
                        this.registerForm = this.formBuilder.group({
                            siteUrl: new forms_1.FormControl({ value: this.siteTenantUrl, disabled: true }, forms_1.Validators.required),
                            ddlTimeZone: ['', [forms_1.Validators.required]],
                            ddlTimeHours: ['', [forms_1.Validators.required]],
                            ddlTimeMinutes: ['', [forms_1.Validators.required]],
                            spUserId: ['', [forms_1.Validators.required, forms_1.Validators.email]],
                            spPassword: ['', forms_1.Validators.required],
                            spConfirmPassword: ['', forms_1.Validators.required],
                            isSameCredential: [false],
                            adgUserId: ['', [forms_1.Validators.required, forms_1.Validators.email]],
                            adgPassword: ['', [forms_1.Validators.required]],
                            adgConfirmPassword: ['', forms_1.Validators.required],
                        });
                        this.loadEventSettingUser();
                    }
                    catch (e) {
                        console.log(e);
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                    }
                };
                OrganizationComponent.prototype.loadTimeZoneForm = function () {
                    this.setTimeZoneddl();
                    this.setTime();
                };
                OrganizationComponent.prototype.setTimeZoneddl = function () {
                    this.getTimeZone();
                };
                OrganizationComponent.prototype.getTimeZone = function () {
                    var _this = this;
                    this._eventSettingService.getTimeZone()
                        .subscribe(function (res) {
                        if (res != null && res != '') {
                            _this.timeZones = JSON.parse(JSON.stringify(res));
                        }
                    });
                };
                OrganizationComponent.prototype.setTime = function () {
                    if (this.availableTimeHours.length == 0) {
                        for (var i = 0; i <= 23; i++) {
                            var hour = i.toString().length == 1 ? "0" + i : i;
                            this.availableTimeHours.push(hour.toString());
                        }
                    }
                    if (this.availableTimeMinutes.length == 0) {
                        for (var i = 0; i <= 59; i++) {
                            var minute = i.toString().length == 1 ? "0" + i : i;
                            this.availableTimeMinutes.push(minute.toString());
                        }
                    }
                };
                Object.defineProperty(OrganizationComponent.prototype, "f", {
                    get: function () { return this.registerForm.controls; },
                    enumerable: true,
                    configurable: true
                });
                OrganizationComponent.prototype.saveUser = function () {
                    var _this = this;
                    try {
                        var objOrganizationUser = new EventSettingUser_1.EventSettingUser();
                        objOrganizationUser.spUserId = this.registerForm.value.spUserId;
                        objOrganizationUser.spUserPassword = this.registerForm.value.spPassword;
                        objOrganizationUser.adgUserPassword = this.registerForm.value.adgPassword;
                        objOrganizationUser.adgUserId = this.registerForm.value.adgUserId;
                        objOrganizationUser.siteUrl = this.siteTenantUrl;
                        var timeZoneDetails_1 = new agentTimeZoneModel_1.AgentTimeZoneModel();
                        timeZoneDetails_1.timeZone = this.registerForm.value.ddlTimeZone;
                        timeZoneDetails_1.time = this.registerForm.value.ddlTimeHours + ":" + this.registerForm.value.ddlTimeMinutes;
                        timeZoneDetails_1.timeZoneDisplayName = this.registerForm.value.ddlTimeZone;
                        objOrganizationUser.agentTimeZoneModel = timeZoneDetails_1;
                        this.timeZones.filter(function (value) {
                            if (value.id == _this.registerForm.value.ddlTimeZone) {
                                timeZoneDetails_1.timeZoneDisplayName = value.displayName;
                            }
                        });
                        this._eventSettingService.saveEventSettingUser(objOrganizationUser)
                            .subscribe(function (res) {
                            if (res != 'fail' && res != '') {
                                _this.isProcessing = false;
                                _this.userFriendlyMessages(false, app_constant_1.CONFIGURATION.constants.messages.SAVED_SUCCESSFULLY);
                                _this.loadEventSettingUser();
                            }
                            else {
                                _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                            }
                        }, function (err) {
                            _this.isProcessing = false;
                            _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                        });
                    }
                    catch (e) {
                        console.log(e);
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                    }
                };
                OrganizationComponent.prototype.loadEventSettingUser = function () {
                    var _this = this;
                    var url = this.siteTenantUrl;
                    this._eventSettingService.getEventSettingUser(url)
                        .subscribe(function (res) {
                        if (res != 'fail' && res != '') {
                            _this.getUserDatacallback(JSON.parse(JSON.stringify(res)));
                        }
                    }, function (err) {
                        console.error(err);
                        _this.isProcessing = false;
                        _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                    });
                };
                OrganizationComponent.prototype.getUserDatacallback = function (user) {
                    this.registerForm.controls['spUserId'].setValue(user.spUserId);
                    this.registerForm.controls['spPassword'].setValue(user.spUserPassword);
                    this.registerForm.controls['spConfirmPassword'].setValue(user.spUserPassword);
                    this.registerForm.controls['adgUserId'].setValue(user.adgUserId);
                    this.registerForm.controls['adgPassword'].setValue(user.adgUserPassword);
                    this.registerForm.controls['adgConfirmPassword'].setValue(user.adgUserPassword);
                    this.registerForm.controls['isSameCredential'].setValue(false);
                    if (user.agentTimeZoneModel) {
                        this.registerForm.controls['ddlTimeZone'].setValue(user.agentTimeZoneModel.timeZone);
                        var time = user.agentTimeZoneModel.time.split(":");
                        if (time) {
                            this.registerForm.controls['ddlTimeHours'].setValue(time[0]);
                            this.registerForm.controls['ddlTimeMinutes'].setValue(time[1]);
                        }
                    }
                    else {
                        this.registerForm.controls['ddlTimeZone'].setValue('');
                        this.registerForm.controls['ddlTimeHours'].setValue('');
                        this.registerForm.controls['ddlTimeMinutes'].setValue('');
                    }
                    this.registerForm.updateValueAndValidity();
                    this.isProcessing = false;
                };
                OrganizationComponent.prototype.onSubmit = function () {
                    this.submitted = true;
                    if (this.registerForm.invalid) {
                        return;
                    }
                    this.isProcessing = true;
                    this.saveUser();
                };
                OrganizationComponent.prototype.goToHome = function () {
                    this._router.navigate(['/dashboard']);
                };
                OrganizationComponent.prototype.resetPage = function () {
                    this.siteTenantUrl = parser_1.Parser.getTenantUrl();
                    this.loadEventSettingUser();
                    this.showAdgSection = false;
                };
                OrganizationComponent.prototype.onNotificationClose = function () {
                    this.showMessageBanner = false;
                };
                OrganizationComponent.prototype.userFriendlyMessages = function (isError, message) {
                    var _this = this;
                    this.messageBannerType = (isError) ? app_constant_1.MessageType[app_constant_1.MessageType.Error] : app_constant_1.MessageType[app_constant_1.MessageType.Notify];
                    this.bannerMessage = message;
                    this.showMessageBanner = true;
                    setTimeout(function () {
                        _this.showMessageBanner = false;
                    }, app_constant_1.CONFIGURATION.constants.MSG_TIMEOUT);
                };
                OrganizationComponent.prototype.checkSameCredential = function () {
                    this.showAdgSection = !this.showAdgSection;
                    if (this.showAdgSection) {
                        this.registerForm.controls['adgUserId'].setValue(this.registerForm.value.spUserId);
                        this.registerForm.controls['adgPassword'].setValue(this.registerForm.value.spPassword);
                        this.registerForm.controls['adgConfirmPassword'].setValue(this.registerForm.value.spConfirmPassword);
                        this.registerForm.updateValueAndValidity();
                    }
                    else {
                        this.registerForm.controls['adgUserId'].setValue('');
                        this.registerForm.controls['adgPassword'].setValue('');
                        this.registerForm.controls['adgConfirmPassword'].setValue('');
                        this.registerForm.updateValueAndValidity();
                    }
                };
                OrganizationComponent = __decorate([
                    core_1.Component({
                        selector: 'organization',
                        templateUrl: './organization.component.html'
                    }),
                    __metadata("design:paramtypes", [windowRef_service_1.WindowRef,
                        router_1.Router,
                        core_1.ElementRef,
                        core_1.Renderer,
                        eventsettinguser_service_1.EventSettingUserService,
                        forms_1.FormBuilder])
                ], OrganizationComponent);
                return OrganizationComponent;
            }());
            exports_1("OrganizationComponent", OrganizationComponent);
        }
    };
});
//# sourceMappingURL=organization.component.js.map