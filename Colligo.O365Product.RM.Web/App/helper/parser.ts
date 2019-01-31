import { LocalStorageUtil } from './localStorageUtil';
import { SharepointRequest } from '../model/sharepointRequest';
import 'rxjs/add/operator/mergeMap';
import { CONFIGURATION } from '../app.constant';

export class Parser {

    static getSharepointRequestObject() {
        let detailJson = localStorage.getItem(CONFIGURATION.localStorage.keys.Node_Detail);
        let spRequest = null;
        if (detailJson) {
            let detailObj = JSON.parse(detailJson);
            spRequest = new SharepointRequest();
            spRequest.name = detailObj.name;
            spRequest.serverRelativeUrl = detailObj.serverRelativeUrl;
            spRequest.url = detailObj.url;
            spRequest.type = detailObj.type;
            spRequest.clientTimeZone = LocalStorageUtil.getValue(CONFIGURATION.localStorage.keys.TIMEZONE);
        }
        return spRequest;
    }


    static getTenantUrl(): string {
        return LocalStorageUtil.getValue(CONFIGURATION.localStorage.keys.Tenant_Url);
    }

    static getCurrentUserId(): number {
        let id: number = 0;
        let data: string = LocalStorageUtil.getValue(CONFIGURATION.localStorage.keys.CurrentLoginUserId);
        if (data) {
            id = parseInt(data);
        }
        return id;
    }
    static getOrganizationMasterId(): number {
        let id: number = 0;
        let data: string = LocalStorageUtil.getValue(CONFIGURATION.localStorage.keys.OrganizationId);
        if (data) {
            id = parseInt(data);
        }
        return id;
    }


    static clearLocalStorageForModeChange() {
        LocalStorageUtil.removeItem(CONFIGURATION.localStorage.keys.Node_Detail);
    }

    static isLogout(): boolean {
        let logout = true;
        let access_token = localStorage.getItem(CONFIGURATION.localStorage.keys.AUTH_TOKEN);
        if (access_token != undefined && access_token !== null && access_token !== '') {
            logout = false;
        }
        return logout;
    }
}
