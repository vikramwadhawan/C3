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
    var core_1, eventReport_1, windowRef_service_1, router_1, dashboard_service_1, app_constant_1, localStorageUtil_1, dashboardSearch_1, parser_1, dateUtil_1, DashboardComponent;
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
            DashboardComponent = (function () {
                function DashboardComponent(_wind, _router, element, _dashboardService) {
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
                    this.showChart = false;
                    this.barChartOptions = {
                        scales: {
                            yAxes: [{
                                    ticks: {
                                        beginAtZero: true
                                    }
                                }]
                        },
                        scaleShowVerticalLines: false,
                        responsive: true,
                    };
                    this.barChartLabels = [];
                    this.barChartType = 'bar';
                    this.barChartLegend = true;
                    this.barChartData = [];
                    this.chartColors = [
                        {
                            backgroundColor: '#ffce56',
                            borderColor: '#ffce56',
                            pointBackgroundColor: '#ffce56',
                            pointBorderColor: '#fff',
                            pointHoverBackgroundColor: '#fff',
                            pointHoverBorderColor: '#ffce56'
                        },
                        {
                            backgroundColor: '#e00030',
                            borderColor: '#e00030',
                            pointBackgroundColor: '#e00030',
                            pointBorderColor: '#fff',
                            pointHoverBackgroundColor: '#fff',
                            pointHoverBorderColor: '#e00030'
                        },
                        {
                            backgroundColor: '#00bb5c',
                            borderColor: '#00bb5c',
                            pointBackgroundColor: '#00bb5c',
                            pointBorderColor: '#fff',
                            pointHoverBackgroundColor: '#fff',
                            pointHoverBorderColor: '#00bb5c'
                        }
                    ];
                    this.endDate = dateUtil_1.DateUtil.getCurrentDateString();
                    this.startDate = dateUtil_1.DateUtil.getFirstDateOfCurrentDateString();
                }
                DashboardComponent.prototype.ngOnInit = function () {
                    this.loadForm();
                };
                DashboardComponent.prototype.refreshDetail = function () {
                    localStorageUtil_1.LocalStorageUtil.removeItem(app_constant_1.CONFIGURATION.localStorage.keys.EventReportSearch);
                    localStorageUtil_1.LocalStorageUtil.removeItem(app_constant_1.CONFIGURATION.localStorage.keys.EventReport);
                    localStorageUtil_1.LocalStorageUtil.removeItem(app_constant_1.CONFIGURATION.localStorage.keys.ErrorLogs);
                    localStorageUtil_1.LocalStorageUtil.removeItem(app_constant_1.CONFIGURATION.localStorage.keys.ErrorLogsPageNumber);
                    localStorageUtil_1.LocalStorageUtil.removeItem(app_constant_1.CONFIGURATION.localStorage.keys.EventSummaryData);
                    this.resetErrorLogs();
                    this.searchEventData();
                    this.showEventSummary();
                };
                DashboardComponent.prototype.showErrorLogs = function (mode) {
                    try {
                        this._router.navigate(["/contentlog", { mode: mode }]);
                    }
                    catch (e) {
                        console.log(e);
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                    }
                };
                DashboardComponent.prototype.onNotificationClose = function () {
                    this.showMessageBanner = false;
                };
                DashboardComponent.prototype.showEventSummary = function () {
                    try {
                        var reportData = localStorageUtil_1.LocalStorageUtil.getValue(app_constant_1.CONFIGURATION.localStorage.keys.EventSummaryData);
                        if (reportData !== undefined && reportData !== null && reportData !== '') {
                            this.setChartData(JSON.parse(reportData));
                        }
                        else
                            this.loadEventSummary();
                    }
                    catch (e) {
                        console.log(e);
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                    }
                };
                DashboardComponent.prototype.resetErrorLogs = function () {
                    this.showChart = false;
                    this.clearChartData();
                };
                DashboardComponent.prototype.generateCriteria = function () {
                    var search = new dashboardSearch_1.DashboardSearch();
                    search.organizationUrl = parser_1.Parser.getTenantUrl();
                    search.status = '-1';
                    search.pageSize = app_constant_1.CONFIGURATION.colligoRMOApp.displayPageSize;
                    this.setDateModel(search);
                    return search;
                };
                DashboardComponent.prototype.setDateModel = function (search) {
                    search.startDate = this.startDate === '' || this.startDate === undefined ? null : new Date(Date.parse(this.startDate));
                    search.endDate = this.endDate === '' || this.endDate === undefined ? dateUtil_1.DateUtil.getCurrentDate() : new Date(Date.parse(this.endDate));
                };
                DashboardComponent.prototype.loadForm = function () {
                    try {
                        var reportData = localStorageUtil_1.LocalStorageUtil.getValue(app_constant_1.CONFIGURATION.localStorage.keys.EventReport);
                        if (reportData !== undefined && reportData !== null && reportData !== '') {
                            this.eventReport = JSON.parse(reportData);
                        }
                        else
                            this.searchEventData();
                        this.showEventSummary();
                    }
                    catch (e) {
                        console.log(e);
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                    }
                };
                DashboardComponent.prototype.loadEventSummary = function () {
                    var search = null;
                    var searchString = localStorageUtil_1.LocalStorageUtil.getValue(app_constant_1.CONFIGURATION.localStorage.keys.EventReportSearch);
                    if (searchString !== undefined && searchString !== null && searchString !== '') {
                        search = JSON.parse(searchString);
                    }
                    if (search === null)
                        search = this.generateCriteria();
                    this.setDateModel(search);
                    this.getEventSummaryFromAPI(search);
                };
                DashboardComponent.prototype.searchEventData = function () {
                    var search = null;
                    var searchString = localStorageUtil_1.LocalStorageUtil.getValue(app_constant_1.CONFIGURATION.localStorage.keys.EventReportSearch);
                    if (searchString !== undefined && searchString !== null && searchString !== '') {
                        search = JSON.parse(searchString);
                    }
                    if (search === null)
                        search = this.generateCriteria();
                    else {
                        this.setDateModel(search);
                    }
                    this.getEventReportFromAPI(search);
                };
                DashboardComponent.prototype.getEventReportFromAPI = function (search) {
                    var _this = this;
                    localStorageUtil_1.LocalStorageUtil.setValue(search, app_constant_1.CONFIGURATION.localStorage.keys.EventReportSearch);
                    this.isProcessing = true;
                    this._dashboardService.searchEventReport(search)
                        .subscribe(function (res) {
                        if (res === undefined) {
                            _this.isProcessing = false;
                            _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                            return;
                        }
                        _this.eventReport = res;
                        localStorageUtil_1.LocalStorageUtil.setValue(res, app_constant_1.CONFIGURATION.localStorage.keys.EventReport);
                        _this.isProcessing = false;
                    }, function (err) {
                        console.error(err);
                        _this.isProcessing = false;
                        _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                    });
                };
                DashboardComponent.prototype.getEventSummaryFromAPI = function (search) {
                    var _this = this;
                    this.isProcessing = true;
                    this._dashboardService.getEventSummary(search)
                        .subscribe(function (res) {
                        if (res === undefined) {
                            _this.isProcessing = false;
                            _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                            return;
                        }
                        if (res !== null && res.length > 0) {
                            _this.setChartData(res);
                        }
                        localStorageUtil_1.LocalStorageUtil.setValue(res, app_constant_1.CONFIGURATION.localStorage.keys.EventSummaryData);
                        _this.isProcessing = false;
                    }, function (err) {
                        console.error(err);
                        _this.isProcessing = false;
                        _this.userFriendlyMessages(true, app_constant_1.CONFIGURATION.constants.messages.Technical_Error);
                    });
                };
                DashboardComponent.prototype.clearChartData = function () {
                    this.barChartLabels = [];
                    this.barChartData = [];
                };
                DashboardComponent.prototype.setChartData = function (data) {
                    this.clearChartData();
                    var itemProcessed = [];
                    var itemError = [];
                    var eventCreated = [];
                    var isMonth = data[0].reportTimeline === 'm';
                    for (var count = 0; count < data.length; count++) {
                        if (isMonth)
                            this.barChartLabels.push(app_constant_1.CONFIGURATION.constants.calendar.Month[data[count].resultSeries - 1]);
                        else
                            this.barChartLabels.push(data[count].resultSeries ? data[count].resultSeries.toString() : '');
                        itemProcessed.push(data[count].itemProcessed);
                        itemError.push(data[count].itemFailed);
                        eventCreated.push(data[count].eventCreated);
                    }
                    this.barChartData.push({ data: itemProcessed, label: 'Item Processed' });
                    this.barChartData.push({ data: itemError, label: 'Item Failed' });
                    this.barChartData.push({ data: eventCreated, label: 'Event Created' });
                    this.showChart = true;
                };
                DashboardComponent.prototype.userFriendlyMessages = function (isError, message) {
                    var _this = this;
                    this.messageBannerType = (isError) ? app_constant_1.MessageType[app_constant_1.MessageType.Error] : app_constant_1.MessageType[app_constant_1.MessageType.Notify];
                    this.bannerMessage = message;
                    this.showMessageBanner = true;
                    setTimeout(function () {
                        _this.showMessageBanner = false;
                    }, app_constant_1.CONFIGURATION.constants.MSG_TIMEOUT);
                };
                DashboardComponent = __decorate([
                    core_1.Component({
                        selector: 'dashboard',
                        templateUrl: './dashboard.component.html'
                    }),
                    __metadata("design:paramtypes", [windowRef_service_1.WindowRef,
                        router_1.Router,
                        core_1.ElementRef,
                        dashboard_service_1.DashboardService])
                ], DashboardComponent);
                return DashboardComponent;
            }());
            exports_1("DashboardComponent", DashboardComponent);
        }
    };
});
//# sourceMappingURL=dashboard.component.js.map