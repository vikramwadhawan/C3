System.register(["@angular/router", "./../../app.constant", "./../../helper/parser", "./../../helper/localStorageUtil", "@angular/core", "../../services/windowRef.service", "../../services/sharepointdata.service", "../../model/eventSetting", "../../services/eventsetting.service", "../../model/eventSetup/eventMappingModel", "@angular/platform-browser"], function (exports_1, context_1) {
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
    var router_1, app_constant_1, parser_1, localStorageUtil_1, core_1, windowRef_service_1, sharepointdata_service_1, eventSetting_1, eventsetting_service_1, eventMappingModel_1, platform_browser_1, EventSetupComponent;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (app_constant_1_1) {
                app_constant_1 = app_constant_1_1;
            },
            function (parser_1_1) {
                parser_1 = parser_1_1;
            },
            function (localStorageUtil_1_1) {
                localStorageUtil_1 = localStorageUtil_1_1;
            },
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (windowRef_service_1_1) {
                windowRef_service_1 = windowRef_service_1_1;
            },
            function (sharepointdata_service_1_1) {
                sharepointdata_service_1 = sharepointdata_service_1_1;
            },
            function (eventSetting_1_1) {
                eventSetting_1 = eventSetting_1_1;
            },
            function (eventsetting_service_1_1) {
                eventsetting_service_1 = eventsetting_service_1_1;
            },
            function (eventMappingModel_1_1) {
                eventMappingModel_1 = eventMappingModel_1_1;
            },
            function (platform_browser_1_1) {
                platform_browser_1 = platform_browser_1_1;
            }
        ],
        execute: function () {
            EventSetupComponent = (function () {
                function EventSetupComponent(_wind, _router, element, _spdataService, _eventSettingService, sanitizer) {
                    this._wind = _wind;
                    this._router = _router;
                    this.element = element;
                    this._spdataService = _spdataService;
                    this._eventSettingService = _eventSettingService;
                    this.sanitizer = sanitizer;
                    this.siteUrl = parser_1.Parser.getTenantUrl();
                    this.siteCollection = [];
                    this.siteMappedCount = [];
                    this.contentType = [];
                    this.eventColumn = [];
                    this.eventColumnDisabled = true;
                    this.contentTypeDisabled = true;
                    this.selectedSiteIndex = 0;
                    this.eventSettings = [];
                    this.selectedSite = null;
                    this.currentSetting = new eventSetting_1.EventSetting();
                    this.isProcessing = false;
                    this.showMessageBanner = false;
                    this.messageBannerType = '';
                    this.bannerMessage = '';
                    this.formControlClass = { 'class': 'form-control' };
                }
                EventSetupComponent.prototype.ngOnInit = function () {
                    this.loadForm();
                };
                EventSetupComponent.prototype.siteChanged = function (options) {
                    this.contentType = [];
                    this.eventColumn = [];
                    this.eventSettings = [];
                    if (options != undefined && options != null && options.length > 0) {
                        var value = options[0].value;
                        for (var index = 0; index < this.siteCollection.length; index++) {
                            var p = this.siteCollection[index];
                            if (p.url === value) {
                                this.selectedSiteIndex = index;
                                this.selectedSite = p;
                                this.contentTypeDisabled = false;
                                if (p.contentType !== undefined && p.contentType !== null && p.contentType.length > 0) {
                                    this.contentType = p.contentType;
                                }
                                else {
                                    try {
                                        this.getContentTypeFromAPI(index, p.url);
                                    }
                                    catch (e) {
                                        console.log(e);
                                        this.isProcessing = false;
                                        this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                                    }
                                }
                                if (p.eventSetting !== undefined && p.eventSetting !== null && p.eventSetting.length > 0) {
                                    this.eventSettings = p.eventSetting;
                                }
                                else {
                                    this.getEventSettingsFromAPI(index, p.url);
                                }
                                break;
                            }
                        }
                    }
                    else
                        this.isProcessing = false;
                };
                EventSetupComponent.prototype.contentTypechanged = function (options) {
                    this.eventColumn = [];
                    if (options != undefined && options != null && options.length > 0) {
                        var value = options[0].value;
                        this.currentSetting.contentType = options[0].text;
                        for (var index = 0; index < this.selectedSite.contentType.length; index++) {
                            var p = this.selectedSite.contentType[index];
                            if (p.id === value) {
                                this.eventColumnDisabled = false;
                                if (p.contentColumns !== undefined && p.contentColumns !== null && p.contentColumns.length > 0) {
                                    this.eventColumn = p.contentColumns;
                                }
                                else {
                                    try {
                                        this.getContentFieldsFromAPI(index, p.id);
                                    }
                                    catch (e) {
                                        this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                                        console.log(e);
                                    }
                                }
                                break;
                            }
                        }
                    }
                };
                EventSetupComponent.prototype.eventColumnChanged = function (options) {
                    if (options != undefined && options != null && options.length > 0) {
                        var value = options[0].value;
                        this.currentSetting.eventColumnName = options[0].text;
                    }
                };
                EventSetupComponent.prototype.saveEventSetting = function () {
                    var _this = this;
                    if (!this.validateEvent())
                        return;
                    try {
                        this.isProcessing = true;
                        this.currentSetting.organizationMasterId = parser_1.Parser.getOrganizationMasterId();
                        this.currentSetting.createdBy = parser_1.Parser.getCurrentUserId();
                        this._eventSettingService.saveEventSetup(this.currentSetting)
                            .subscribe(function (res) {
                            if (res !== 'success') {
                                _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                                _this.isProcessing = false;
                                return;
                            }
                            _this.eventSettings.push(_this.currentSetting);
                            _this.updateSiteMappedCount(_this.currentSetting.contentSiteUrl);
                            _this.clearForm();
                            _this.userFriendlyMessages(false, app_constant_1.CONFIGURATION.constants.messages.SAVED_SUCCESSFULLY);
                        }, function (err) {
                            console.error(err);
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
                EventSetupComponent.prototype.updateEventSetting = function (rowData) {
                    var _this = this;
                    try {
                        this.isProcessing = true;
                        var data_1 = new eventSetting_1.EventSetting();
                        data_1.eventSettingId = rowData.eventSettingId;
                        data_1.isActive = !rowData.isActive;
                        data_1.lastRetrivalTime = rowData.lastRetrivalTime;
                        data_1.createdBy = parser_1.Parser.getCurrentUserId();
                        this._eventSettingService.updateEventSetup(data_1)
                            .subscribe(function (res) {
                            if (res !== 'success') {
                                _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                                _this.isProcessing = false;
                                return;
                            }
                            _this.userFriendlyMessages(false, app_constant_1.CONFIGURATION.constants.messages.SAVED_SUCCESSFULLY);
                            var result = _this.eventSettings.filter(function (p) { return p.eventSettingId === rowData.eventSettingId; });
                            result[0].isActive = data_1.isActive;
                            _this.isProcessing = false;
                        }, function (err) {
                            console.error(err);
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
                EventSetupComponent.prototype.clearForm = function () {
                    this.eventColumnDisabled = true;
                    this.contentTypeDisabled = true;
                    this.selectedSiteIndex = 0;
                    this.selectedSite = null;
                    this.currentSetting = new eventSetting_1.EventSetting();
                    this.currentSetting.isActive = true;
                    this.isProcessing = false;
                };
                EventSetupComponent.prototype.updateSiteMappedCount = function (url) {
                    var insert = true;
                    if (this.siteMappedCount !== null && this.siteMappedCount.length > 0) {
                        for (var count = 0; count < this.siteMappedCount.length; count++) {
                            if (url === this.siteMappedCount[count].contentSiteUrl) {
                                this.siteMappedCount[count].mappedCount = this.siteMappedCount[count].mappedCount + 1;
                                localStorageUtil_1.LocalStorageUtil.setValue(this.siteMappedCount, app_constant_1.CONFIGURATION.localStorage.keys.EventMappedCount);
                                insert = false;
                                break;
                            }
                        }
                    }
                    else
                        this.siteMappedCount = [];
                    if (insert) {
                        var mappingCount = new eventMappingModel_1.EventMappingCount();
                        mappingCount.contentSiteUrl = url;
                        mappingCount.mappedCount = 1;
                        mappingCount.organizationRootUrl = parser_1.Parser.getTenantUrl();
                        this.siteMappedCount.push(mappingCount);
                        localStorageUtil_1.LocalStorageUtil.setValue(this.siteMappedCount, app_constant_1.CONFIGURATION.localStorage.keys.EventMappedCount);
                    }
                };
                EventSetupComponent.prototype.resetForm = function () {
                    this.clearForm();
                    this.eventColumn = [];
                    this.contentType = [];
                    this.eventSettings = [];
                };
                EventSetupComponent.prototype.cancelForm = function () {
                    this._router.navigate(["/dashboard"]);
                };
                EventSetupComponent.prototype.hasEventSettings = function () {
                    return this.eventSettings !== undefined && this.eventSettings !== null && this.eventSettings.length > 0;
                };
                EventSetupComponent.prototype.isEventMapped = function (url) {
                    var result = false;
                    if (this.siteMappedCount !== null && this.siteMappedCount.length > 0) {
                        for (var count = 0; count < this.siteMappedCount.length; count++) {
                            if (url === this.siteMappedCount[count].contentSiteUrl) {
                                result = true;
                                break;
                            }
                        }
                    }
                    return result;
                };
                EventSetupComponent.prototype.getMappingCount = function (url) {
                    var result = '';
                    if (this.siteMappedCount !== null && this.siteMappedCount.length > 0) {
                        for (var count = 0; count < this.siteMappedCount.length; count++) {
                            if (url === this.siteMappedCount[count].contentSiteUrl) {
                                result = '(' + this.siteMappedCount[count].mappedCount + ')';
                                break;
                            }
                        }
                    }
                    return result;
                };
                EventSetupComponent.prototype.style = function (data) {
                    return this.sanitizer.bypassSecurityTrustStyle(data);
                };
                EventSetupComponent.prototype.validateEvent = function () {
                    var _this = this;
                    var result = true;
                    if (this.currentSetting === undefined || this.currentSetting === null || (this.currentSetting !== null
                        && (this.currentSetting.eventColumnInternalName === undefined || this.currentSetting.contentSiteUrl === undefined || this.currentSetting.contentTypeId === undefined))) {
                        result = false;
                        this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Event_Setting.Invalid_Data);
                    }
                    else {
                        if (this.eventSettings && this.eventSettings.length > 0) {
                            var duplicateEvent = this.eventSettings.filter(function (p) { return p.contentTypeId === _this.currentSetting.contentTypeId &&
                                p.eventColumnInternalName === _this.currentSetting.eventColumnInternalName && p.contentSiteUrl === _this.currentSetting.contentSiteUrl; });
                            if (duplicateEvent && duplicateEvent.length > 0) {
                                result = false;
                                this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Event_Setting.Duplicate_Event);
                            }
                        }
                    }
                    return result;
                };
                EventSetupComponent.prototype.loadForm = function () {
                    try {
                        this.currentSetting.isActive = true;
                        var sites = localStorageUtil_1.LocalStorageUtil.getValue(app_constant_1.CONFIGURATION.localStorage.keys.EventSettings);
                        var getSites = false;
                        if (sites !== undefined && sites !== null && sites !== '') {
                            var siteList = JSON.parse(sites);
                            if (siteList !== undefined && siteList !== null && siteList.length > 0) {
                                this.siteCollection = siteList;
                                var eventMappedCount = localStorageUtil_1.LocalStorageUtil.getValue(app_constant_1.CONFIGURATION.localStorage.keys.EventMappedCount);
                                if (eventMappedCount !== undefined && eventMappedCount !== null && eventMappedCount !== '') {
                                    this.siteMappedCount = JSON.parse(eventMappedCount);
                                }
                            }
                            else
                                getSites = true;
                        }
                        else
                            getSites = true;
                        if (getSites)
                            this.getSharePointSitesFromAPI();
                    }
                    catch (e) {
                        console.log(e);
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                    }
                };
                EventSetupComponent.prototype.getSharePointSitesFromAPI = function () {
                    var _this = this;
                    var spRequest = parser_1.Parser.getSharepointRequestObject();
                    if (spRequest) {
                        this.isProcessing = true;
                        this._spdataService.loadSharePointSites(spRequest)
                            .subscribe(function (res) {
                            _this.siteCollection = res;
                            localStorageUtil_1.LocalStorageUtil.setValue(res, app_constant_1.CONFIGURATION.localStorage.keys.EventSettings);
                            _this._eventSettingService.getEventSettingCount(spRequest.url).subscribe(function (data) {
                                _this.siteMappedCount = data;
                                localStorageUtil_1.LocalStorageUtil.setValue(data, app_constant_1.CONFIGURATION.localStorage.keys.EventMappedCount);
                                _this.isProcessing = false;
                            }, function (err) {
                                console.error(err);
                                _this.isProcessing = false;
                                _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                            });
                        }, function (err) {
                            console.error(err);
                            _this.isProcessing = false;
                            _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                        });
                    }
                    else {
                        console.log('sharepoint request object not found');
                    }
                };
                EventSetupComponent.prototype.getEventSettingsFromAPI = function (index, url) {
                    var _this = this;
                    try {
                        this.isProcessing = true;
                        this._eventSettingService.loadAllEventSettingsByOrgUrl(url)
                            .subscribe(function (res) {
                            _this.siteCollection[index].eventSetting = res;
                            _this.eventSettings = res;
                            _this.isProcessing = false;
                            localStorageUtil_1.LocalStorageUtil.setValue(_this.siteCollection, app_constant_1.CONFIGURATION.localStorage.keys.EventSettings);
                        }, function (err) {
                            console.error(err);
                            _this.isProcessing = false;
                            _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                        });
                    }
                    catch (e) {
                        console.log(e);
                        this.isProcessing = false;
                    }
                };
                EventSetupComponent.prototype.getContentTypeFromAPI = function (index, url) {
                    var _this = this;
                    var spRequest = parser_1.Parser.getSharepointRequestObject();
                    if (spRequest) {
                        this.isProcessing = true;
                        spRequest.url = url;
                        this._spdataService.loadContentType(spRequest)
                            .subscribe(function (res) {
                            _this.siteCollection[index].contentType = res;
                            _this.contentType = res;
                            localStorageUtil_1.LocalStorageUtil.setValue(_this.siteCollection, app_constant_1.CONFIGURATION.localStorage.keys.EventSettings);
                            _this.isProcessing = false;
                        }, function (err) {
                            console.error(err);
                            _this.isProcessing = false;
                            _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                        });
                    }
                    else {
                        console.log('sharepoint request object not found');
                    }
                };
                EventSetupComponent.prototype.getContentFieldsFromAPI = function (index, id) {
                    var _this = this;
                    var spRequest = parser_1.Parser.getSharepointRequestObject();
                    if (spRequest) {
                        this.isProcessing = true;
                        spRequest.url = this.siteCollection[this.selectedSiteIndex].url;
                        spRequest.contentTypeId = id;
                        this._spdataService.loadMetadataFields(spRequest)
                            .subscribe(function (res) {
                            _this.eventColumn = res;
                            _this.siteCollection[_this.selectedSiteIndex].contentType[index].contentColumns = res;
                            localStorageUtil_1.LocalStorageUtil.setValue(_this.siteCollection, app_constant_1.CONFIGURATION.localStorage.keys.EventSettings);
                            _this.isProcessing = false;
                        }, function (err) {
                            console.error(err);
                            _this.isProcessing = false;
                            _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                        });
                    }
                    else {
                        console.log('sharepoint request object not found');
                    }
                };
                EventSetupComponent.prototype.onNotificationClose = function () {
                    this.showMessageBanner = false;
                };
                EventSetupComponent.prototype.userFriendlyMessages = function (isError, message) {
                    var _this = this;
                    this.messageBannerType = (isError) ? app_constant_1.MessageType[app_constant_1.MessageType.Error] : app_constant_1.MessageType[app_constant_1.MessageType.Notify];
                    this.bannerMessage = message;
                    this.showMessageBanner = true;
                    setTimeout(function () {
                        _this.showMessageBanner = false;
                    }, app_constant_1.CONFIGURATION.constants.MSG_TIMEOUT);
                };
                EventSetupComponent = __decorate([
                    core_1.Component({
                        selector: 'eventsetup',
                        templateUrl: './eventsetup.component.html',
                        encapsulation: core_1.ViewEncapsulation.None
                    }),
                    __metadata("design:paramtypes", [windowRef_service_1.WindowRef,
                        router_1.Router,
                        core_1.ElementRef,
                        sharepointdata_service_1.SharepointdataService,
                        eventsetting_service_1.EventSettingService,
                        platform_browser_1.DomSanitizer])
                ], EventSetupComponent);
                return EventSetupComponent;
            }());
            exports_1("EventSetupComponent", EventSetupComponent);
        }
    };
});
//# sourceMappingURL=eventsetup.component.js.map