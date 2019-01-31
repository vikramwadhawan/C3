import { Component, OnInit, ElementRef } from '@angular/core';
import { EventReport } from '../../model/dashboard/eventReport';
import { WindowRef } from '../../services/windowRef.service';
import { Router } from '@angular/router';
import { DashboardService } from '../../services/dashboard.service';
import { CONFIGURATION, MessageType } from '../../app.constant';
import { LocalStorageUtil } from '../../helper/localStorageUtil';
import { DashboardSearch } from '../../model/dashboard/dashboardSearch';
import { Parser } from './../../helper/parser';
import { DateUtil } from './../../helper/dateUtil';
import { EventSummaryModel } from '../../model/dashboard/eventSummaryModel';
import { DatePipe } from '@angular/common';

@Component({
    selector: 'dashboard',
    templateUrl: './dashboard.component.html'
})
export class DashboardComponent implements OnInit {

    constructor(private _wind: WindowRef,
        private _router: Router,
        private element: ElementRef,
        private _dashboardService: DashboardService,
    ) {
        this.endDate = DateUtil.getCurrentDateString();
        this.startDate = DateUtil.getFirstDateOfCurrentDateString();
    }

    public isProcessing: boolean = false;
    public showMessageBanner: boolean = false;
    public messageBannerType: string = '';
    public bannerMessage: string = '';
    public eventReport: EventReport = new EventReport();

    public throttle: number = 100;
    public scrollDistance: number = 0;  
    public startDate: string = null;
    public endDate: string = null;


    public showChart: boolean = false;
    public barChartOptions: any = {
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
    public barChartLabels: string[] = [];
    public barChartType: string = 'bar';
    public barChartLegend: boolean = true;
    public barChartData: any[] = [];
    public chartColors: any[] = [
        { // item processed
            backgroundColor: '#ffce56',
            borderColor: '#ffce56',
            pointBackgroundColor: '#ffce56',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: '#ffce56'
        },
        { // item failed
            backgroundColor: '#e00030',
            borderColor: '#e00030',
            pointBackgroundColor: '#e00030',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: '#e00030'
        },
        { // event created
            backgroundColor: '#00bb5c',
            borderColor: '#00bb5c',
            pointBackgroundColor: '#00bb5c',
            pointBorderColor: '#fff',
            pointHoverBackgroundColor: '#fff',
            pointHoverBorderColor: '#00bb5c'
        }
    ]

    datePipe: DatePipe;


    ngOnInit() {
        this.loadForm();
    }

    public refreshDetail() {
        LocalStorageUtil.removeItem(CONFIGURATION.localStorage.keys.EventReportSearch);
        LocalStorageUtil.removeItem(CONFIGURATION.localStorage.keys.EventReport);
        LocalStorageUtil.removeItem(CONFIGURATION.localStorage.keys.ErrorLogs);
        LocalStorageUtil.removeItem(CONFIGURATION.localStorage.keys.ErrorLogsPageNumber);
        LocalStorageUtil.removeItem(CONFIGURATION.localStorage.keys.EventSummaryData);
        this.resetErrorLogs();
        this.searchEventData();
        this.showEventSummary();
    }

    public showErrorLogs(mode: string) {
        try {
            this._router.navigate(["/contentlog", { mode: mode}]);

        } catch (e) {
            console.log(e);
            this.isProcessing = false;
            this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
        }
    }

    onNotificationClose() {
        this.showMessageBanner = false;
    }

    private showEventSummary() {
        try {
            let reportData: string = LocalStorageUtil.getValue(CONFIGURATION.localStorage.keys.EventSummaryData);
            if (reportData !== undefined && reportData !== null && reportData !== '') {
                this.setChartData(<EventSummaryModel[]>JSON.parse(reportData));
            }
            else
                this.loadEventSummary();

        } catch (e) {
            console.log(e);
            this.isProcessing = false;
            this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
        }
    }

    private resetErrorLogs() {
        this.showChart = false;
        this.clearChartData();
    }

    private generateCriteria(): DashboardSearch {
        let search: DashboardSearch = new DashboardSearch();
        search.organizationUrl = Parser.getTenantUrl();
        search.status = '-1';
        search.pageSize = CONFIGURATION.colligoRMOApp.displayPageSize;
        this.setDateModel(search);
        return search;
    }

    private setDateModel(search: DashboardSearch) {
        search.startDate = this.startDate === '' || this.startDate === undefined ? null : new Date(Date.parse(this.startDate));
        search.endDate = this.endDate === '' || this.endDate === undefined ? DateUtil.getCurrentDate() : new Date(Date.parse(this.endDate));
    }

    private loadForm() {
        try {
            let reportData: string = LocalStorageUtil.getValue(CONFIGURATION.localStorage.keys.EventReport);
            if (reportData !== undefined && reportData !== null && reportData !== '') {
                this.eventReport = JSON.parse(reportData);
            }
            else
                this.searchEventData();
            this.showEventSummary();

        } catch (e) {
            console.log(e);
            this.isProcessing = false;
            this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
        }
    }

    private loadEventSummary() {
        let search: DashboardSearch = null;
        let searchString: string = LocalStorageUtil.getValue(CONFIGURATION.localStorage.keys.EventReportSearch);
        if (searchString !== undefined && searchString !== null && searchString !== '') {
            search = JSON.parse(searchString);
        }
        if (search === null)
            search = this.generateCriteria();
        this.setDateModel(search);
        this.getEventSummaryFromAPI(search);
    }

    private searchEventData() {
        let search: DashboardSearch = null;
        let searchString: string = LocalStorageUtil.getValue(CONFIGURATION.localStorage.keys.EventReportSearch);
        if (searchString !== undefined && searchString !== null && searchString !== '') {
            search = JSON.parse(searchString);
        }
        if (search === null)
            search = this.generateCriteria();
        else {
            this.setDateModel(search);
        }
        this.getEventReportFromAPI(search);
    }

    private getEventReportFromAPI(search: DashboardSearch) {
        LocalStorageUtil.setValue(search, CONFIGURATION.localStorage.keys.EventReportSearch);
        this.isProcessing = true;
        this._dashboardService.searchEventReport(search)
            .subscribe((res) => {
                if (res === undefined) {
                    this.isProcessing = false;
                    this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                    return;
                }
                this.eventReport = res;
                LocalStorageUtil.setValue(res, CONFIGURATION.localStorage.keys.EventReport);
                this.isProcessing = false;
            },
                (err) => {
                    console.error(err);
                    this.isProcessing = false;
                    this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                });
    }

    private getEventSummaryFromAPI(search: DashboardSearch) {
        this.isProcessing = true;
        this._dashboardService.getEventSummary(search)
            .subscribe((res) => {
                if (res === undefined) {
                    this.isProcessing = false;
                    this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                    return;
                }
                if (res !== null && res.length > 0) {
                    this.setChartData(res);
                }
                LocalStorageUtil.setValue(res, CONFIGURATION.localStorage.keys.EventSummaryData);
                this.isProcessing = false;
            },
                (err) => {
                    console.error(err);
                    this.isProcessing = false;
                    this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                });
    }

    private clearChartData() {
        this.barChartLabels = [];
        this.barChartData = [];
    }

    private setChartData(data: EventSummaryModel[]) {
        this.clearChartData();
        let itemProcessed: Array<number> = [];
        let itemError: Array<number> = [];
        let eventCreated: Array<number> = [];
        let isMonth: boolean = data[0].reportTimeline === 'm';
        for (let count: number = 0; count < data.length; count++) {
            if (isMonth)
                this.barChartLabels.push(CONFIGURATION.constants.calendar.Month[data[count].resultSeries - 1]);
            else
                this.barChartLabels.push(data[count].resultSeries ? data[count].resultSeries.toString() : '')
            itemProcessed.push(data[count].itemProcessed);
            itemError.push(data[count].itemFailed);
            eventCreated.push(data[count].eventCreated);
        }
        this.barChartData.push({ data: itemProcessed, label: 'Item Processed' });
        this.barChartData.push({ data: itemError, label: 'Item Failed' });
        this.barChartData.push({ data: eventCreated, label: 'Event Created' });
        this.showChart = true;
    }

    private userFriendlyMessages(isError: boolean, message: string) {
        this.messageBannerType = (isError) ? MessageType[MessageType.Error] : MessageType[MessageType.Notify];
        this.bannerMessage = message;
        this.showMessageBanner = true;
        setTimeout(() => {
            this.showMessageBanner = false;
        }, CONFIGURATION.constants.MSG_TIMEOUT)
    }

}

