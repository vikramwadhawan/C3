import { ServiceBase } from "./service.base";
import { SharepointRequest } from "../model/sharepointRequest";
import { Observable } from "rxjs";
import { Injectable } from "@angular/core";
import { ContentType } from "../model/eventSetup/contentTypeModel";
import { CONFIGURATION } from "../app.constant";
import { Field } from "../model/eventSetup/fieldModel";

@Injectable()
export class SharepointdataService extends ServiceBase {

    loadSharePointSites(spRequest: SharepointRequest): Observable<SharepointRequest[]> {

        if (spRequest) {
            return this.tryGetSPToken(spRequest).flatMap(
                (data) => {
                    if (data !== null) {
                        spRequest.accessToken = data;
                        spRequest.idToken = this.token;
                        return this._http.post(CONFIGURATION.colligoRMOApp.searchSharepointSite, JSON.stringify(spRequest), this.requestOptions)
                            .map((res) => {
                                return <SharepointRequest[]>JSON.parse(res.json());
                            });
                    }
                    else {
                        this.ReturnError(CONFIGURATION.constants.messages.SP_TOKEN_MISSING);
                    }
                });
        }
        else {
            return this.ReturnError(CONFIGURATION.constants.messages.REQUEST_OBJECT_MISSING);
        }
    }

    loadContentType(spRequest: SharepointRequest): Observable<ContentType[]> {

        if (spRequest) {
            return this.tryGetSPToken(spRequest).flatMap(
                (data) => {
                    if (data !== null) {
                        spRequest.accessToken = data;
                        spRequest.idToken = this.token;
                        return this._http.post(CONFIGURATION.colligoRMOApp.getAllContentLog, JSON.stringify(spRequest), this.requestOptions)
                            .map((res) => {
                                return <ContentType[]>JSON.parse(res.json());
                            });
                    }
                    else {
                        this.ReturnError(CONFIGURATION.constants.messages.SP_TOKEN_MISSING);
                    }
                });
        }
        else {
            return this.ReturnError(CONFIGURATION.constants.messages.REQUEST_OBJECT_MISSING);
        }
    }

    loadMetadataFields(spRequest: SharepointRequest): Observable<Field[]> {

        if (spRequest) {
            return this.tryGetSPToken(spRequest).flatMap(
                (data) => {
                    if (data !== null) {
                        spRequest.accessToken = data;
                        spRequest.idToken = this.token;
                        return this._http.post(CONFIGURATION.colligoRMOApp.getAllFieldsByContentId, JSON.stringify(spRequest), this.requestOptions)
                            .map((res) => {
                                return <Field[]>JSON.parse(res.json());
                            });
                    }
                    else {
                        this.ReturnError(CONFIGURATION.constants.messages.SP_TOKEN_MISSING);
                    }
                });
        }
        else {
            return this.ReturnError(CONFIGURATION.constants.messages.REQUEST_OBJECT_MISSING);
        }
    }

}