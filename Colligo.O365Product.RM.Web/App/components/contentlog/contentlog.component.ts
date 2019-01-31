import { Component, OnInit, ElementRef } from '@angular/core';
import { EventReport } from '../../model/dashboard/eventReport';
import { WindowRef } from '../../services/windowRef.service';
import { Router, ActivatedRoute } from '@angular/router';
import { DashboardService } from '../../services/dashboard.service';
import { CONFIGURATION, MessageType } from '../../app.constant';
import { LocalStorageUtil } from '../../helper/localStorageUtil';
import { DashboardSearch } from '../../model/dashboard/dashboardSearch';
import { Parser } from './../../helper/parser';
import { DateUtil } from './../../helper/dateUtil';
import { DashboardContentLog } from '../../model/dashboard/dashboardContentLog';
import { EventSummaryModel } from '../../model/dashboard/eventSummaryModel';
import { DatePipe } from '@angular/common';
import { ContentSummaryModel } from '../../model/dashboard/contentSummaryModel';

@Component({
    selector: 'contentlog',
    templateUrl: './contentlog.component.html'
})
export class ContentLogComponent implements OnInit {

    constructor(private _wind: WindowRef,
        private route: ActivatedRoute,
        private _router: Router,
        private element: ElementRef,
        private _dashboardService: DashboardService,
    ) {
        this.route.params.subscribe(param => {
            if (param['mode']) {
                this.mode = param['mode'];
            }
        });
        this.endDate = DateUtil.getCurrentDateString();
        this.startDate = DateUtil.getFirstDateOfCurrentDateString();
    }

    public isProcessing: boolean = false;
    public isFullyErrorContentLoad: boolean = false;
    public showLogs: boolean = false;
    public showMessageBanner: boolean = false;
    public messageBannerType: string = '';
    public bannerMessage: string = '';
    public eventReport: EventReport = new EventReport();
    public errorLogs: Array<DashboardContentLog> = [];
    public throttle: number = 100;
    public scrollDistance: number = 0;
    public pagingNumber: number = 1;
    public startDate: string = null;
    public endDate: string = null;
    public mode: string = 'error';
    private key: string = '';
    private pageKey: string = '';

    datePipe: DatePipe;

    ngOnInit() {
        this.loadForm();
    }

    onErrorLogScrollDown() {
        this.pagingNumber += 1;
        this.loadErrorLogs(this.pagingNumber);
    }

    onNotificationClose() {
        this.showMessageBanner = false;
    }

    backForm() {
        this._router.navigate(["/dashboard"]);
    }

    private resetErrorLogs() {
        this.isFullyErrorContentLoad = false;
        this.showLogs = false;
        this.errorLogs = [];
    }

    private generateCriteria(): DashboardSearch {
        let search: DashboardSearch = new DashboardSearch();
        search.organizationUrl = Parser.getTenantUrl();
        search.status = '-1';
        search.pageSize = CONFIGURATION.colligoRMOApp.displayPageSize;
        return search;
    }

    private loadForm() {
        try {
            this.key = CONFIGURATION.localStorage.keys.ItemProcessedToday;
            this.pageKey = CONFIGURATION.localStorage.keys.ItemProcessedTodayPageNumber;
            if (this.mode === 'error') {
                this.key = CONFIGURATION.localStorage.keys.ErrorLogs;
                this.pageKey = CONFIGURATION.localStorage.keys.ErrorLogsPageNumber;
            }

            let reportData: string = LocalStorageUtil.getValue(this.key);
            if (reportData !== undefined && reportData !== null && reportData !== '') {
                this.showLogs = true;
                this.errorLogs = <DashboardContentLog[]>JSON.parse(reportData);
                let currentPageNumber: string = LocalStorageUtil.getValue(this.pageKey);
                if (currentPageNumber !== undefined && currentPageNumber !== null)
                    this.pagingNumber = parseInt(currentPageNumber);
            }
            else
                this.loadErrorLogs(1);

        } catch (e) {
            console.log(e);
            this.isProcessing = false;
            this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
        }
    }

    private loadErrorLogs(pagenumber: number) {
        this.showLogs = true;
        let search: DashboardSearch = null;
        let searchString: string = LocalStorageUtil.getValue(CONFIGURATION.localStorage.keys.EventReportSearch);
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
            search.startDate = DateUtil.getCurrentDate();
            search.endDate = DateUtil.getCurrentDate();
        }
        search.pageNumber = pagenumber;
        this.getErrorLogsFromAPI(search);
    }

    private getErrorLogsFromAPI(search: DashboardSearch) {
        this.isProcessing = true;
        this._dashboardService.getAllDashboardContentLog(search)
            .subscribe((res) => {
                if (res === undefined) {
                    this.isProcessing = false;
                    this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                    return;
                }
                if (res !== null && res.length > 0) {
                    for (let count = 0; count < res.length; count++)
                        this.errorLogs.push(res[count]);
                }
                else
                    this.isFullyErrorContentLoad = true;

                LocalStorageUtil.setValue(this.errorLogs, this.key);
                localStorage.setItem(this.pageKey, this.pagingNumber.toString());
                this.isProcessing = false;
            },
                (err) => {
                    console.error(err);
                    this.isProcessing = false;
                    this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                });
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

