import { Observable } from 'rxjs/Observable';
import { Router } from '@angular/router';
import { CONFIGURATION, MessageType } from './../../app.constant';
import { SharepointRequest } from './../../model/sharepointRequest';
import { Parser } from './../../helper/parser';
import { LocalStorageUtil } from './../../helper/localStorageUtil';
import { Component, OnInit, OnDestroy, AfterViewInit, ElementRef, ViewEncapsulation } from '@angular/core';
import { WindowRef } from '../../services/windowRef.service';
import { forEach } from '@angular/router/src/utils/collection';
import { AppUtil } from '../../helper/appUtil';
import { SharepointdataService } from '../../services/sharepointdata.service';
import { ContentType } from "../../model/eventSetup/contentTypeModel";
import { INgxSelectOption } from 'ngx-select-ex';
import { Field } from '../../model/eventSetup/fieldModel';
import { EventSetting } from '../../model/eventSetting';
import { EventSettingService } from '../../services/eventsetting.service';
import { noUndefined } from '@angular/compiler/src/util';
import { EventMappingCount } from '../../model/eventSetup/eventMappingModel';
import { SafeStyle, DomSanitizer } from '@angular/platform-browser';

declare var $: any;

@Component({
    selector: 'eventsetup',
    templateUrl: './eventsetup.component.html',
    encapsulation: ViewEncapsulation.None
})

export class EventSetupComponent implements OnInit {

    constructor(private _wind: WindowRef,
        private _router: Router,
        private element: ElementRef,
        private _spdataService: SharepointdataService,
        private _eventSettingService: EventSettingService,
        public sanitizer: DomSanitizer
    ) {
    }

    siteUrl: string = Parser.getTenantUrl();
    public siteCollection: Array<SharepointRequest> = [];
    public siteMappedCount: Array<EventMappingCount> = [];
    public contentType: Array<ContentType> = [];
    public eventColumn: Array<Field> = [];
    public eventColumnDisabled = true;
    public contentTypeDisabled = true;
    selectedSiteIndex: number = 0;
    public eventSettings: Array<EventSetting> = [];
    public selectedSite: SharepointRequest = null;
    public currentSetting: EventSetting = new EventSetting();
    public isProcessing: boolean = false;
    public showMessageBanner: boolean = false;
    public messageBannerType: string = '';
    public bannerMessage: string = '';
    public formControlClass: any = { 'class': 'form-control' };
    /**Lifecycle hooks */
    ngOnInit() {
        this.loadForm();
    }

    public siteChanged(options: INgxSelectOption[]) {
        this.contentType = [];
        this.eventColumn = [];
        this.eventSettings = [];
        if (options != undefined && options != null && options.length > 0) {
            let value = options[0].value;
            for (var index = 0; index < this.siteCollection.length; index++) {
                let p = this.siteCollection[index];
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
                        } catch (e) {
                            console.log(e);
                            this.isProcessing = false;
                            this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
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
    }

    public contentTypechanged(options: INgxSelectOption[]) {
        this.eventColumn = [];
        if (options != undefined && options != null && options.length > 0) {
            let value = options[0].value;
            this.currentSetting.contentType = options[0].text;
            for (var index = 0; index < this.selectedSite.contentType.length; index++) {
                let p = this.selectedSite.contentType[index];
                if (p.id === value) {
                    this.eventColumnDisabled = false;
                    if (p.contentColumns !== undefined && p.contentColumns !== null && p.contentColumns.length > 0) {
                        this.eventColumn = p.contentColumns;
                    }
                    else {
                        try {
                            this.getContentFieldsFromAPI(index, p.id);
                        } catch (e) {
                            this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                            console.log(e);
                        }
                    }
                    break;
                }
            }
        }
    }

    public eventColumnChanged(options: INgxSelectOption[]) {
        if (options != undefined && options != null && options.length > 0) {
            let value = options[0].value;
            this.currentSetting.eventColumnName = options[0].text;
        }
    }

    public saveEventSetting() {
        if (!this.validateEvent())
            return;
        try {
            this.isProcessing = true;
            this.currentSetting.organizationMasterId = Parser.getOrganizationMasterId();
            this.currentSetting.createdBy = Parser.getCurrentUserId();
            this._eventSettingService.saveEventSetup(this.currentSetting)
                .subscribe((res) => {
                    if (res !== 'success') {
                        this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                        this.isProcessing = false;
                        return;
                    }
                    this.eventSettings.push(this.currentSetting);
                    this.updateSiteMappedCount(this.currentSetting.contentSiteUrl);
                    this.clearForm();
                    this.userFriendlyMessages(false, CONFIGURATION.constants.messages.SAVED_SUCCESSFULLY);
                },
                    (err) => {
                        console.error(err);
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                    });
        } catch (e) {
            console.log(e);
            this.isProcessing = false;
            this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
        }

    }

    public updateEventSetting(rowData: EventSetting) {
        try {
            this.isProcessing = true;
            let data: EventSetting = new EventSetting();
            data.eventSettingId = rowData.eventSettingId;
            data.isActive = !rowData.isActive;
            data.lastRetrivalTime = rowData.lastRetrivalTime;
            data.createdBy = Parser.getCurrentUserId();
            this._eventSettingService.updateEventSetup(data)
                .subscribe((res) => {
                    if (res !== 'success') {
                        this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                        this.isProcessing = false;
                        return;
                    }
                    this.userFriendlyMessages(false, CONFIGURATION.constants.messages.SAVED_SUCCESSFULLY);
                    let result = this.eventSettings.filter(p => p.eventSettingId === rowData.eventSettingId);
                    result[0].isActive = data.isActive;
                    this.isProcessing = false;
                },
                    (err) => {
                        console.error(err);
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                    });
        } catch (e) {
            console.log(e);
            this.isProcessing = false;
            this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
        }

    }

    private clearForm() {
        this.eventColumnDisabled = true;
        this.contentTypeDisabled = true;
        this.selectedSiteIndex = 0;
        this.selectedSite = null;
        this.currentSetting = new EventSetting();
        this.currentSetting.isActive = true;
        this.isProcessing = false;
    }

    private updateSiteMappedCount(url: string) {
        let insert: boolean = true;
        if (this.siteMappedCount !== null && this.siteMappedCount.length > 0) {
            for (let count = 0; count < this.siteMappedCount.length; count++) {
                if (url === this.siteMappedCount[count].contentSiteUrl) {
                    this.siteMappedCount[count].mappedCount = this.siteMappedCount[count].mappedCount + 1;
                    LocalStorageUtil.setValue(this.siteMappedCount, CONFIGURATION.localStorage.keys.EventMappedCount);
                    insert = false;
                    break;
                }
            }
        }
        else
            this.siteMappedCount = [];
        if (insert) {
            let mappingCount = new EventMappingCount();
            mappingCount.contentSiteUrl = url;
            mappingCount.mappedCount = 1;
            mappingCount.organizationRootUrl = Parser.getTenantUrl();
            this.siteMappedCount.push(mappingCount);
            LocalStorageUtil.setValue(this.siteMappedCount, CONFIGURATION.localStorage.keys.EventMappedCount);
        }
    }


    public resetForm() {
        this.clearForm();
        this.eventColumn = [];
        this.contentType = [];
        this.eventSettings = [];
    }

    public cancelForm() {
        this._router.navigate(["/dashboard"]);
    }

    public hasEventSettings(): boolean {
        return this.eventSettings !== undefined && this.eventSettings !== null && this.eventSettings.length > 0;
    }


    public isEventMapped(url: string): boolean {
        let result: boolean = false;
        if (this.siteMappedCount !== null && this.siteMappedCount.length > 0) {
            for (let count = 0; count < this.siteMappedCount.length; count++) {
                if (url === this.siteMappedCount[count].contentSiteUrl) {
                    result = true;
                    break;
                }
            }
        }
        return result;
    }

    public getMappingCount(url: string): string {
        let result: string = '';
        if (this.siteMappedCount !== null && this.siteMappedCount.length > 0) {
            for (let count = 0; count < this.siteMappedCount.length; count++) {
                if (url === this.siteMappedCount[count].contentSiteUrl) {
                    result = '(' + this.siteMappedCount[count].mappedCount + ')';
                    break;
                }
            }
        }
        return result;
    }

    style(data: string): SafeStyle {
        return this.sanitizer.bypassSecurityTrustStyle(data);
    }

    private validateEvent(): boolean {
        let result: boolean = true;
        if (this.currentSetting === undefined || this.currentSetting === null || (this.currentSetting !== null
            && (this.currentSetting.eventColumnInternalName === undefined || this.currentSetting.contentSiteUrl === undefined || this.currentSetting.contentTypeId === undefined))) {
            result = false;
            this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Event_Setting.Invalid_Data);
        }
        else {
            if (this.eventSettings && this.eventSettings.length > 0) {
                let duplicateEvent: EventSetting[] = this.eventSettings.filter(p => p.contentTypeId === this.currentSetting.contentTypeId &&
                    p.eventColumnInternalName === this.currentSetting.eventColumnInternalName && p.contentSiteUrl === this.currentSetting.contentSiteUrl);
                if (duplicateEvent && duplicateEvent.length > 0) {
                    result = false;
                    this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Event_Setting.Duplicate_Event);
                }
            }
        }

        return result;
    }

    private loadForm() {
        try {
            this.currentSetting.isActive = true;
            let sites: string = LocalStorageUtil.getValue(CONFIGURATION.localStorage.keys.EventSettings);
            let getSites: boolean = false;
            if (sites !== undefined && sites !== null && sites !== '') {
                let siteList: Array<SharepointRequest> = JSON.parse(sites);
                if (siteList !== undefined && siteList !== null && siteList.length > 0) {
                    this.siteCollection = siteList;
                    let eventMappedCount: string = LocalStorageUtil.getValue(CONFIGURATION.localStorage.keys.EventMappedCount);
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
        } catch (e) {
            console.log(e);
            this.isProcessing = false;
            this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
        }
    }

    private getSharePointSitesFromAPI() {
        let spRequest: SharepointRequest = Parser.getSharepointRequestObject();
        if (spRequest) {
            this.isProcessing = true;
            this._spdataService.loadSharePointSites(spRequest)
                .subscribe((res) => {
                    this.siteCollection = res;
                    LocalStorageUtil.setValue(res, CONFIGURATION.localStorage.keys.EventSettings);
                    this._eventSettingService.getEventSettingCount(spRequest.url).subscribe(
                        (data) => {
                            this.siteMappedCount = data;
                            LocalStorageUtil.setValue(data, CONFIGURATION.localStorage.keys.EventMappedCount);
                            this.isProcessing = false;
                        },
                        (err) => {
                            console.error(err);
                            this.isProcessing = false;
                            this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                        });
                },
                    (err) => {
                        console.error(err);
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                    });
        }
        else {
            console.log('sharepoint request object not found');
        }
    }

    private getEventSettingsFromAPI(index: number, url: string) {
        try {
            this.isProcessing = true;
            this._eventSettingService.loadAllEventSettingsByOrgUrl(url)
                .subscribe((res) => {
                    this.siteCollection[index].eventSetting = res;
                    this.eventSettings = res;
                    this.isProcessing = false;
                    LocalStorageUtil.setValue(this.siteCollection, CONFIGURATION.localStorage.keys.EventSettings);
                },
                    (err) => {
                        console.error(err);
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                    });
        } catch (e) {
            console.log(e);
            this.isProcessing = false;
        }
    }

    private getContentTypeFromAPI(index: number, url: string) {
        let spRequest: SharepointRequest = Parser.getSharepointRequestObject();
        if (spRequest) {
            this.isProcessing = true;
            spRequest.url = url;
            this._spdataService.loadContentType(spRequest)
                .subscribe((res) => {
                    this.siteCollection[index].contentType = res;
                    this.contentType = res;
                    LocalStorageUtil.setValue(this.siteCollection, CONFIGURATION.localStorage.keys.EventSettings);
                    this.isProcessing = false;
                },
                    (err) => {
                        console.error(err);
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                    });
        }
        else {
            console.log('sharepoint request object not found');
        }
    }

    private getContentFieldsFromAPI(index: number, id: string) {
        let spRequest: SharepointRequest = Parser.getSharepointRequestObject();
        if (spRequest) {
            this.isProcessing = true;
            spRequest.url = this.siteCollection[this.selectedSiteIndex].url;
            spRequest.contentTypeId = id;
            this._spdataService.loadMetadataFields(spRequest)
                .subscribe((res) => {
                    this.eventColumn = res;
                    this.siteCollection[this.selectedSiteIndex].contentType[index].contentColumns = res;
                    LocalStorageUtil.setValue(this.siteCollection, CONFIGURATION.localStorage.keys.EventSettings);
                    this.isProcessing = false;
                },
                    (err) => {
                        console.error(err);
                        this.isProcessing = false;
                        this.userFriendlyMessages(true, CONFIGURATION.constants.messages.Technical_Error);
                    });
        }
        else {
            console.log('sharepoint request object not found');
        }
    }

    onNotificationClose() {
        this.showMessageBanner = false;
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
