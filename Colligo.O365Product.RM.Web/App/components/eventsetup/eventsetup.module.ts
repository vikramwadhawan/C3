import { EventSetupComponent } from './eventsetup.component';
import { appRouting } from './../../app.routing';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { NgxSelectModule } from 'ngx-select-ex';
import { SharepointdataService } from '../../services/sharepointdata.service';
import { EventSettingService } from '../../services/eventsetting.service';
import { SharedModule } from '../shared/shared.module';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        FormsModule,
        ReactiveFormsModule,
        appRouting,
        InfiniteScrollModule,
        NgxSelectModule,
        SharedModule
    ],
    exports: [

        EventSetupComponent
    ],
    declarations: [

        EventSetupComponent
    ],
    providers: [
        SharepointdataService,
        EventSettingService
    ],
})
export class EventSetupModule { }
