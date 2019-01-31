System.register(["@angular/core", "../../model/dashboard/eventReport", "../../services/windowRef.service", "@angular/router", "../../services/dashboard.service", "../../app.constant", "../../model/dashboard/dashboardSearch", "./../../helper/parser", "./../../helper/dateUtil"], function (exports_1, context_1) {
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
    var core_1, eventReport_1, windowRef_service_1, router_1, dashboard_service_1, app_constant_1, dashboardSearch_1, parser_1, dateUtil_1, AuditHistoryComponent;
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
            AuditHistoryComponent = (function () {
                function AuditHistoryComponent(_wind, _router, element, _dashboardService) {
                    this._wind = _wind;
                    this._router = _router;
                    this.element = element;
                    this._dashboardService = _dashboardService;
                    this.isProcessing = false;
                    this.showMessageBanner = false;
                    this.messageBannerType = '';
                    this.bannerMessage = '';
                    this.eventReport = new eventReport_1.EventReport();
                    this.throttle = 100;
                    this.scrollDistance = 0;
                    this.startDate = null;
                    this.endDate = null;
                    this.showContentSummary = false;
                    this.isFullyAuditContentLoad = false;
                    this.auditLogs = [];
                    this.searchTerm = '';
                    this.auditSummaryPagingNumber = 1;
                    this.endDate = dateUtil_1.DateUtil.getCurrentDateString();
                    this.startDate = dateUtil_1.DateUtil.getFirstDateOfCurrentDateString();
                }
                AuditHistoryComponent.prototype.ngOnInit = function () {
                };
                AuditHistoryComponent.prototype.showAuditLog = function () {
                    this.resetAuditLogs(false);
                    this.loadAuditLogs(1);
                };
                AuditHistoryComponent.prototype.onNotificationClose = function () {
                    this.showMessageBanner = false;
                };
                AuditHistoryComponent.prototype.onAuditLogScrollDown = function () {
                    this.auditSummaryPagingNumber += 1;
                    this.loadAuditLogs(this.auditSummaryPagingNumber);
                };
                AuditHistoryComponent.prototype.loadAdgStatus = function (status, desc) {
                    if (status === 'Error')
                        return status + ' -- ' + desc;
                    return status;
                };
                AuditHistoryComponent.prototype.resetAuditLogs = function (clearSearch) {
                    this.isFullyAuditContentLoad = false;
                    this.showContentSummary = false;
                    this.auditLogs = [];
                    this.auditSummaryPagingNumber = 1;
                    this.endDate = dateUtil_1.DateUtil.getCurrentDateString();
                    this.startDate = dateUtil_1.DateUtil.getFirstDateOfCurrentDateString();
                    if (clearSearch)
                        this.searchTerm = '';
                };
                AuditHistoryComponent.prototype.generateCriteria = function () {
                    var search = new dashboardSearch_1.DashboardSearch();
                    search.organizationUrl = parser_1.Parser.getTenantUrl();
                    search.status = '-1';
                    search.pageSize = app_constant_1.CONFIGURATION.colligoRMOApp.displayPageSize;
                    this.setDateModel(search);
                    return search;
                };
                AuditHistoryComponent.prototype.setDateModel = function (search) {
                    search.startDate = this.startDate === '' || this.startDate === undefined ? null : new Date(Date.parse(this.startDate));
                    search.endDate = this.endDate === '' || this.endDate === undefined ? dateUtil_1.DateUtil.getCurrentDate() : new Date(Date.parse(this.endDate));
                };
                AuditHistoryComponent.prototype.loadAuditLogs = function (pagenumber) {
                    this.showContentSummary = true;
                    var search = this.generateCriteria();
                    search.endDate = null;
                    search.startDate = null;
                    search.searchTerm = this.searchTerm;
                    search.pageNumber = pagenumber;
                    this.getAuditLogsFromAPI(search);
                };
                AuditHistoryComponent.prototype.getAuditLogsFromAPI = function (search) {
                    var _this = this;
                    this.isProcessing = true;
                    this._dashboardService.getContentAuditSummary(search)
                        .subscribe(function (res) {
                        if (res === undefined) {
                            _this.isProcessing = false;
                            _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                            return;
                        }
                        if (res !== null && res.length > 0) {
                            for (var count = 0; count < res.length; count++)
                                _this.auditLogs.push(res[count]);
                        }
                        else
                            _this.isFullyAuditContentLoad = true;
                        _this.isProcessing = false;
                    }, function (err) {
                        console.error(err);
                        _this.isProcessing = false;
                        _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                    });
                };
                AuditHistoryComponent.prototype.userFriendlyMessages = function (isError, message) {
                    var _this = this;
                    this.messageBannerType = (isError) ? app_constant_1.MessageType[app_constant_1.MessageType.Error] : app_constant_1.MessageType[app_constant_1.MessageType.Notify];
                    this.bannerMessage = message;
                    this.showMessageBanner = true;
                    setTimeout(function () {
                        _this.showMessageBanner = false;
                    }, app_constant_1.CONFIGURATION.constants.MSG_TIMEOUT);
                };
                AuditHistoryComponent = __decorate([
                    core_1.Component({
                        selector: 'auditlog',
                        templateUrl: './audithistory.component.html'
                    }),
                    __metadata("design:paramtypes", [windowRef_service_1.WindowRef,
                        router_1.Router,
                        core_1.ElementRef,
                        dashboard_service_1.DashboardService])
                ], AuditHistoryComponent);
                return AuditHistoryComponent;
            }());
            exports_1("AuditHistoryComponent", AuditHistoryComponent);
        }
    };
});
//# sourceMappingURL=audithistory.component.js.map