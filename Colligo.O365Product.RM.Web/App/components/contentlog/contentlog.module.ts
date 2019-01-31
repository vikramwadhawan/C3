import { ContentLogComponent } from './contentlog.component';
import { appRouting } from './../../app.routing';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { DashboardService } from '../../services/dashboard.service';
import { SharedModule } from '../shared/shared.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
//import { ChartsModule } from 'ng2-charts';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        appRouting,        
        SharedModule,
        InfiniteScrollModule,
        FormsModule
    ],
    exports: [
        ContentLogComponent
    ],
    declarations: [
        ContentLogComponent
    ],
    providers: [
        DashboardService
    ],
})
export class ContentLogModule { }
