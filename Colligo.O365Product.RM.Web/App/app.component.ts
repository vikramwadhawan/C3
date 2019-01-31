import { Component } from '@angular/core';
import { Router, NavigationEnd } from "@angular/router";
import { Observable } from "rxjs/Observable";
import { CONFIGURATION } from './app.constant';
import { ContentNode } from './model/contentNode';
import { Parser } from './helper/parser';
import { LocalStorageUtil } from './helper/localStorageUtil';
@Component({
    selector: 'rmo-app',
    templateUrl: './app.component.html'
})

export class AppComponent {
    private _router: Router;
    public showMenu: boolean = true;
    constructor(public router: Router) {
        this.router.routeReuseStrategy.shouldReuseRoute = function () {
            return false;
        }
        this.router.events.subscribe(event => {
            if (event instanceof NavigationEnd) {
                // trick the Router into believing it's last link wasn't previously loaded
                this.router.navigated = false;
                // if you need to scroll back to top, here is the right place
                window.scrollTo(0, 0);
            }
        });
        this._router = router;
        this.setContentNode();
        console.log('rmo app triggered');

    }

    setContentNode() {
        let node: ContentNode = new ContentNode();
        node.name = '';
        node.serverRelativeUrl = '/';
        node.url = Parser.getTenantUrl();
        LocalStorageUtil.setValue(node, CONFIGURATION.localStorage.keys.Node_Detail);
    }
    public onDisconnect() {
        localStorage.removeItem(CONFIGURATION.localStorage.keys.AUTH_TOKEN);
        window.location.href = '/Login/Logout';
    }

    public changeMenu() {
        this.showMenu = !this.showMenu;
    }
}