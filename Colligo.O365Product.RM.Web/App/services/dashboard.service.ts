import { ServiceBase } from "./service.base";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { CONFIGURATION } from "../app.constant";
import { Headers } from '@angular/http';
import { DashboardSearch } from "../model/dashboard/dashboardSearch";
import { EventReport } from "../model/dashboard/eventReport";
import { DashboardContentLog } from "../model/dashboard/dashboardContentLog";
import { EventSummaryModel } from "../model/dashboard/eventSummaryModel";
import { ContentSummaryModel } from "../model/dashboard/contentSummaryModel";

@Injectable()
export class DashboardService extends ServiceBase {

    searchEventReport(search: DashboardSearch): Observable<EventReport> {
        if (search) {
            let headers = new Headers();
            headers.append('Content-Type', 'application/json');
            return this._http.post(CONFIGURATION.colligoRMOApp.searchEventReport, { 'criteria': JSON.stringify(search) }, { headers: headers })
                .map((res: any) => {
                    let data = JSON.parse(res.json());
                    return data;
                })
        }
    }

    getAllDashboardContentLog(search: DashboardSearch): Observable<DashboardContentLog[]> {
        if (search) {
            let headers = new Headers();
            headers.append('Content-Type', 'application/json');
            return this._http.post(CONFIGURATION.colligoRMOApp.getAllDashboardContentLog, { 'criteria': JSON.stringify(search) }, { headers: headers })
                .map((res: any) => {
                    return <DashboardContentLog[]>JSON.parse(res.json());
                })
        }
    }

    getEventSummary(search: DashboardSearch): Observable<EventSummaryModel[]> {
        if (search) {
            let headers = new Headers();
            headers.append('Content-Type', 'application/json');
            return this._http.post(CONFIGURATION.colligoRMOApp.getEventSummary, { 'criteria': JSON.stringify(search) }, { headers: headers })
                .map((res: any) => {
                    return <EventSummaryModel[]>JSON.parse(res.json());
                })
        }
    }

    getContentAuditSummary(search: DashboardSearch): Observable<ContentSummaryModel[]> {
        if (search) {
            let headers = new Headers();
            headers.append('Content-Type', 'application/json');
            return this._http.post(CONFIGURATION.colligoRMOApp.getContentAuditSummary, { 'criteria': JSON.stringify(search) }, { headers: headers })
                .map((res: any) => {
                    return <ContentSummaryModel[]>JSON.parse(res.json());
                })
        }
    }
}
