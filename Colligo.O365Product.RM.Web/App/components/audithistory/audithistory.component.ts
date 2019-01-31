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
import { ContentSummaryModel } from '../../model/dashboard/contentSummaryModel';

@Component({
    selector: 'auditlog',
    templateUrl: './audithistory.component.html'
})
export class AuditHistoryComponent implements OnInit {

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

    public showContentSummary: boolean = false;
    public isFullyAuditContentLoad: boolean = false;
    public auditLogs: Array<ContentSummaryModel> = [];
    public searchTerm: string = '';
    public auditSummaryPagingNumber: number = 1;


    ngOnInit() {
    }

    public showAuditLog() {
        this.resetAuditLogs(false);
        this.loadAuditLogs(1);
    }

    onNotificationClose() {
        this.showMessageBanner = false;
    }

    onAuditLogScrollDown() {
        this.auditSummaryPagingNumber += 1;
        this.loadAuditLogs(this.auditSummaryPagingNumber);
    }

    loadAdgStatus(status: string, desc: string) {
        if (status === 'Error')
            return status + ' -- ' + desc;
        return status;
    }

    public resetAuditLogs(clearSearch: boolean) {
        this.isFullyAuditContentLoad = false;
        this.showContentSummary = false;
        this.auditLogs = [];
        this.auditSummaryPagingNumber = 1;
        this.endDate = DateUtil.getCurrentDateString();
        this.startDate = DateUtil.getFirstDateOfCurrentDateString();
        if (clearSearch)
            this.searchTerm = '';
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

    private loadAuditLogs(pagenumber: number) {
        this.showContentSummary = true;
        let search: DashboardSearch = this.generateCriteria();
        search.endDate = null;
        search.startDate = null;
        search.searchTerm = this.searchTerm;
        search.pageNumber = pagenumber;
        this.getAuditLogsFromAPI(search);
    }

    private getAuditLogsFromAPI(search: DashboardSearch) {
        this.isProcessing = true;
        this._dashboardService.getContentAuditSummary(search)
            .subscribe((res) => {
                if (res === undefined) {
                    this.isProcessing = false;
                    this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                    return;
                }
                if (res !== null && res.length > 0) {
                    for (let count = 0; count < res.length; count++)
                        this.auditLogs.push(res[count]);
                }
                else
                    this.isFullyAuditContentLoad = true;
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

