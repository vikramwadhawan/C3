import { ServiceBase } from "./service.base";
import { Headers } from '@angular/http';
import { SharepointRequest } from "../model/sharepointRequest";
import { EventSettingUser } from "../model/eventSettingUserSetup/eventSettingUser";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { CONFIGURATION } from "../app.constant";


@Injectable()
export class EventSettingUserService extends ServiceBase {

    saveEventSettingUser(data: EventSettingUser): Observable<string> {

        if (data !== null) {
            let headers = new Headers();
            headers.append('Content-Type', 'application/json');
            return this._http.post(CONFIGURATION.colligoRMOApp.saveEventSettingUser, { 'data': JSON.stringify(data) }, { headers: headers })
                .map(res => res.text());
        }
        else {
            return this.ReturnError(CONFIGURATION.constants.messages.REQUEST_OBJECT_MISSING);
        }

    }


    getEventSettingUser(siteUrl: string): Observable<string> {

        if (siteUrl.length > 0) {
            return this._http.get(CONFIGURATION.colligoRMOApp.getEventSettingUser + "?siteUrl=" +encodeURI(siteUrl), this.requestOptions)
                .map((res) => {
                    return <string>JSON.parse(res.json());
                });
        }
        else {
            return this.ReturnError(CONFIGURATION.constants.messages.REQUEST_OBJECT_MISSING);
        }

    }
    
    getTimeZone(): Observable<string> {

        return this._http.get(CONFIGURATION.colligoRMOApp.getTimeZone, this.requestOptions)
                .map((res) => {
                  return <string>JSON.parse(res.json());
                });
    }
   
}