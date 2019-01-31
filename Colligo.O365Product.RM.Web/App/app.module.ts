
import { NgModule } from '@angular/core';
import { APP_BASE_HREF } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OrganizationModule } from './components/organization/organization.module';
import { EventSetupModule } from './components/eventsetup/eventsetup.module';
import { DashboardModule } from './components/dashboard/dashboard.module';
import { AuditHistoryModule } from './components/audithistory/audithistory.module';
import { ContentLogModule } from './components/contentlog/contentlog.module';
import { AppComponent } from './app.component';
import { appRouting } from './app.routing';
import { WindowRef } from './services/windowRef.service';
import { SharedModule } from './components/shared/shared.module';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        appRouting,
        EventSetupModule,
        OrganizationModule,       
        DashboardModule,
        AuditHistoryModule,
        ContentLogModule,
        SharedModule
       
    ],
    declarations: [
        AppComponent
    ],
    providers: [
        { provide: APP_BASE_HREF, useValue: '/' },
        WindowRef
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
