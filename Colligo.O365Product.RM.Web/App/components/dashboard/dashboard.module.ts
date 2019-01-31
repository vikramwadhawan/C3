import { DashboardComponent } from './dashboard.component';
import { appRouting } from './../../app.routing';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ChartsModule } from 'ng4-charts/ng4-charts';
import { DashboardService } from '../../services/dashboard.service';
import { SharedModule } from '../shared/shared.module';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
//import { ChartsModule } from 'ng2-charts';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        appRouting,
        ChartsModule,
        SharedModule,
        InfiniteScrollModule,
        FormsModule
    ],
    exports: [
        DashboardComponent
    ],
    declarations: [
        DashboardComponent
    ],
    providers: [
        DashboardService
    ],
})
export class DashboardModule { }
