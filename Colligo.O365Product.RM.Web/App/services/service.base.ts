import { RequestOptionsArgs, Headers } from '@angular/http';
import { Observable } from "rxjs/Observable";
import { CONFIGURATION } from '../app.constant';
import { SharepointRequest } from './../model/sharepointRequest';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { Injectable } from '@angular/core';
import { log } from 'util';
import { ResourceContext } from '../model/resourceContext';
import { Parser } from '../helper/parser';

@Injectable()
export class ServiceBase {
    token: string;
    //defaultToken: string= CONFIGURATION.token;
    defaultToken: string;
    requestOptions: RequestOptionsArgs;
    tenentUrl: string;
    constructor(public _http: Http) {
        this.setHeaderOptions();
        this.tenentUrl = localStorage.getItem(CONFIGURATION.localStorage.keys.Tenant_Url);
    }

    tryGetToken(): string {
        let token = this.defaultToken;
        let access_token = localStorage.getItem(CONFIGURATION.localStorage.keys.AUTH_TOKEN);
        if (access_token) {
            token = access_token
        }
        return token;
    }

    setHeaderOptions() {
        this.token = this.tryGetToken();
        this.requestOptions = { headers: new Headers() };
        this.requestOptions.headers.set('Authorization', `Bearer ${this.token}`);
        this.requestOptions.headers.set('Content-Type', `application/json; charset=utf-8`);
    }

    /**
     * get sp token for the request
     * @param spRequest
     */
    tryGetSPToken(spRequest: SharepointRequest): Observable<string> {
        return this.tryGetSPTokenByUrl(spRequest.url);
    }

    tryGetSPTokenByUrl(url: string): Observable<string> {
        if (!Parser.isLogout()) {
            let token: string = null;
            if (url) {
                let headers = new Headers();
                headers.append('Content-Type', 'application/json');
                let requestUrl = CONFIGURATION.colligoRMOApp.getSharePointRefreshToken + '?url=' + encodeURIComponent(url);
                return this._http.get(requestUrl, { headers: headers })
                    .map((res: any) => {
                        let data = res.json();
                        return data.data;
                    })
            }
        }
        else {
            window.location.href = '/Login/Logout';
        }
    }


    /**
     * return error 
     * @param errorMessage
     */
    ReturnError(errorMessage: string) {
        console.error(errorMessage);
        return Observable.throw(errorMessage);
    }

}
