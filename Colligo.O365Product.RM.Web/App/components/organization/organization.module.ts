import { OrganizationComponent } from './organization.component';
import { EqualValidatorDirective } from '../../directive/equalvalidator.directive'
import { appRouting } from './../../app.routing';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { NgxSelectModule } from 'ngx-select-ex';
import { NgModule } from '@angular/core';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { EventSettingUserService } from '../../services/eventsettinguser.service'
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

        OrganizationComponent
    ],
    declarations: [

        OrganizationComponent
        ,EqualValidatorDirective
    ],
    providers: [
        EventSettingUserService
   ],
})
export class OrganizationModule { }
