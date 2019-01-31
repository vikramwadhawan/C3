import { ServiceBase } from "./service.base";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CONFIGURATION } from "../app.constant";
import { Headers } from '@angular/http';
import { SharepointRequest } from "../model/sharepointRequest";
import { EventSetting } from "../model/eventSetting";
import { EventMappingCount } from "../model/eventSetup/eventMappingModel";

@Injectable()
export class EventSettingService extends ServiceBase {

    saveEventSetup(context: EventSetting): Observable<string> {
        if (context) {
            let headers = new Headers();
            headers.append('Content-Type', 'application/json');
            return this._http.post(CONFIGURATION.colligoRMOApp.saveEventSetup, { 'setup': JSON.stringify(context) }, { headers: headers })
                .map((res: any) => {
                    let data = res.json();
                    return data;
                })
        }
    }

    updateEventSetup(context: EventSetting): Observable<string> {
        if (context) {
            let headers = new Headers();
            headers.append('Content-Type', 'application/json');
            return this._http.post(CONFIGURATION.colligoRMOApp.updateEventSetup, { 'setup': JSON.stringify(context) }, { headers: headers })
                .map((res: any) => {
                    let data = res.json();
                    return data;
                })
        }
    }

    loadAllEventSettingsByOrgUrl(url: string): Observable<EventSetting[]> {
        if (url) {
            let param: string = CONFIGURATION.colligoRMOApp.getAllEventSetupByOrgUrl + "?url=" + encodeURIComponent(url);
            return this._http.get(param, this.requestOptions)
                .map(response => {
                    return <EventSetting[]>JSON.parse(response.json());
                })
        }
    }

    getEventSettingCount(url: string): Observable<EventMappingCount[]> {
        if (url) {
            let param: string = CONFIGURATION.colligoRMOApp.getEventSettingCount + "?url=" + encodeURIComponent(url);
            return this._http.get(param, this.requestOptions)
                .map(response => {
                    return <EventMappingCount[]>JSON.parse(response.json());
                })
        }
    }
}