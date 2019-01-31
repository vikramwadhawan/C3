
import { RouterModule } from '@angular/router';
import { ModuleWithProviders } from "@angular/core/src/metadata";
import { EventSetupComponent } from './components/eventsetup/eventsetup.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { OrganizationComponent } from './components/organization/organization.component';
import { AuditHistoryComponent } from './components/audithistory/audithistory.component';
import { ContentLogComponent } from './components/contentlog/contentlog.component';



const routes = [
    { path: 'organization', component: OrganizationComponent },
    { path: 'eventsetup', component: EventSetupComponent },
    { path: 'dashboard', component: DashboardComponent },
    { path: 'auditlog', component: AuditHistoryComponent },
    { path: 'contentlog', component: ContentLogComponent },
    { path: '**', redirectTo: 'dashboard' }
];

export const appRouting: ModuleWithProviders = RouterModule.forRoot(routes, { useHash: true })

