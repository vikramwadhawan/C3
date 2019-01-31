System.register(["@angular/core", "../../model/dashboard/eventReport", "../../services/windowRef.service", "@angular/router", "../../services/dashboard.service", "../../app.constant", "../../helper/localStorageUtil", "../../model/dashboard/dashboardSearch", "./../../helper/parser", "./../../helper/dateUtil"], function (exports_1, context_1) {
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
    var core_1, eventReport_1, windowRef_service_1, router_1, dashboard_service_1, app_constant_1, localStorageUtil_1, dashboardSearch_1, parser_1, dateUtil_1, ContentLogComponent;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (eventReport_1_1) {
                eventReport_1 = eventReport_1_1;
            },
            function (windowRef_service_1_1) {
                windowRef_service_1 = windowRef_service_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (dashboard_service_1_1) {
                dashboard_service_1 = dashboard_service_1_1;
            },
            function (app_constant_1_1) {
                app_constant_1 = app_constant_1_1;
            },
            function (localStorageUtil_1_1) {
                localStorageUtil_1 = localStorageUtil_1_1;
            },
            function (dashboardSearch_1_1) {
                dashboardSearch_1 = dashboardSearch_1_1;
            },
            function (parser_1_1) {
                parser_1 = parser_1_1;
            },
            function (dateUtil_1_1) {
                dateUtil_1 = dateUtil_1_1;
            }
        ],
        execute: function () {
            ContentLogComponent = (function () {
                function ContentLogComponent(_wind, route, _router, element, _dashboardService) {
                    var _this = this;
                    this._wind = _wind;
                    this.route = route;
                    this._router = _router;
                    this.element = element;
                    this._dashboardService = _dashboardService;
                    this.isProcessing = false;
                    this.isFullyErrorContentLoad = false;
                    this.showLogs = false;
                    this.showMessageBanner = false;
                    this.messageBannerType = '';
                    this.bannerMessage = '';
                    this.eventReport = new eventReport_1.EventReport();
                    this.errorLogs = [];
                    this.throttle = 100;
                    this.scrollDistance = 0;
                    this.pagingNumber = 1;
                    this.startDate = null;
                    this.endDate = null;
                    this.mode = 'error';
                    this.key = '';
                    this.pageKey = '';
                    this.route.params.subscribe(function (param) {
                        if (param['mode']) {
                            _this.mode = param['mode'];
                        }
                    });
                    this.endDate = dateUtil_1.DateUtil.getCurrentDateString();
                    this.startDate = dateUtil_1.DateUtil.getFirstDateOfCurrentDateString();
                }
                ContentLogComponent.prototype.ngOnInit = function () {
                    this.loadForm();
                };
                ContentLogComponent.prototype.onErrorLogScrollDown = function () {
                    this.pagingNumber += 1;
                    this.loadErrorLogs(this.pagingNumber);
                };
                ContentLogComponent.prototype.onNotificationClose = function () {
                    this.showMessageBanner = false;
                };
                ContentLogComponent.prototype.backForm = function () {
                    this._router.navigate(["/dashboard"]);
                };
                ContentLogComponent.prototype.resetErrorLogs = function () {
                    this.isFullyErrorContentLoad = false;
                    this.showLogs = false;
                    this.errorLogs = [];
                };
                ContentLogComponent.prototype.generateCriteria = function () {
                    var search = new dashboardSearch_1.DashboardSearch();
                    search.organizationUrl = parser_1.Parser.getTenantUrl();
                    search.status = '-1';
                    search.pageSize = app_constant_1.CONFIGURATION.colligoRMOApp.displayPageSize;
                    return search;
                };
                ContentLogComponent.prototype.loadForm = function () {
                    try {
                        this.key = app_constant_1.CONFIGURATION.localStorage.keys.ItemProcessedToday;
                        this.pageKey = app_constant_1.CONFIGURATION.localStorage.keys.ItemProcessedTodayPageNumber;
                        if (this.mode === 'error') {
                            this.key = app_constant_1.CONFIGURATION.localStorage.keys.ErrorLogs;
                            this.pageKey = app_constant_1.CONFIGURATION.localStorage.keys.ErrorLogsPageNumber;
                        }
                        var reportData = localStorageUtil_1.LocalStorageUtil.getValue(this.key);
                        if (reportData !== undefined && reportData !== null && reportData !== '') {
                            this.showLogs = true;
                            this.errorLogs = JSON.parse(reportData);
                            var currentPageNumber = localStorageUtil_1.LocalStorageUtil.getValue(this.pageKey);
                            if (currentPageNumber !== undefined && currentPageNumber !== null)
                                this.pagingNumber = parseInt(currentPageNumber);
                        }
                        else
                            this.loadErrorLogs(1);
                    }
                    catch (e) {
                        console.log(e);
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                    }
                };
                ContentLogComponent.prototype.loadErrorLogs = function (pagenumber) {
                    this.showLogs = true;
                    var search = null;
                    var searchString = localStorageUtil_1.LocalStorageUtil.getValue(app_constant_1.CONFIGURATION.localStorage.keys.EventReportSearch);
                    if (searchString !== undefined && searchString !== null && searchString !== '') {
                        search = JSON.parse(searchString);
                    }
                    if (search === null)
                        search = this.generateCriteria();
                    if (this.mode === 'error') {
                        search.errorRecord = true;
                        search.startDate = null;
                    }
                    else {
                        search.errorRecord = false;
                        search.startDate = dateUtil_1.DateUtil.getCurrentDate();
                        search.endDate = dateUtil_1.DateUtil.getCurrentDate();
                    }
                    search.pageNumber = pagenumber;
                    this.getErrorLogsFromAPI(search);
                };
                ContentLogComponent.prototype.getErrorLogsFromAPI = function (search) {
                    var _this = this;
                    this.isProcessing = true;
                    this._dashboardService.getAllDashboardContentLog(search)
                        .subscribe(function (res) {
                        if (res === undefined) {
                            _this.isProcessing = false;
                            _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                            return;
                        }
                        if (res !== null && res.length > 0) {
                            for (var count = 0; count < res.length; count++)
                                _this.errorLogs.push(res[count]);
                        }
                        else
                            _this.isFullyErrorContentLoad = true;
                        localStorageUtil_1.LocalStorageUtil.setValue(_this.errorLogs, _this.key);
                        localStorage.setItem(_this.pageKey, _this.pagingNumber.toString());
                        _this.isProcessing = false;
                    }, function (err) {
                        console.error(err);
                        _this.isProcessing = false;
                        _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                    });
                };
                ContentLogComponent.prototype.userFriendlyMessages = function (isError, message) {
                    var _this = this;
                    this.messageBannerType = (isError) ? app_constant_1.MessageType[app_constant_1.MessageType.Error] : app_constant_1.MessageType[app_constant_1.MessageType.Notify];
                    this.bannerMessage = message;
                    this.showMessageBanner = true;
                    setTimeout(function () {
                        _this.showMessageBanner = false;
                    }, app_constant_1.CONFIGURATION.constants.MSG_TIMEOUT);
                };
                ContentLogComponent = __decorate([
                    core_1.Component({
                        selector: 'contentlog',
                        templateUrl: './contentlog.component.html'
                    }),
                    __metadata("design:paramtypes", [windowRef_service_1.WindowRef,
                        router_1.ActivatedRoute,
                        router_1.Router,
                        core_1.ElementRef,
                        dashboard_service_1.DashboardService])
                ], ContentLogComponent);
                return ContentLogComponent;
            }());
            exports_1("ContentLogComponent", ContentLogComponent);
        }
    };
});
//# sourceMappingURL=contentlog.component.js.map