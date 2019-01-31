System.register(["@angular/router", "./components/eventsetup/eventsetup.component", "./components/dashboard/dashboard.component", "./components/organization/organization.component", "./components/audithistory/audithistory.component", "./components/contentlog/contentlog.component"], function (exports_1, context_1) {
    "use strict";
    var router_1, eventsetup_component_1, dashboard_component_1, organization_component_1, audithistory_component_1, contentlog_component_1, routes, appRouting;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (eventsetup_component_1_1) {
                eventsetup_component_1 = eventsetup_component_1_1;
            },
            function (dashboard_component_1_1) {
                dashboard_component_1 = dashboard_component_1_1;
            },
            function (organization_component_1_1) {
                organization_component_1 = organization_component_1_1;
            },
            function (audithistory_component_1_1) {
                audithistory_component_1 = audithistory_component_1_1;
            },
            function (contentlog_component_1_1) {
                contentlog_component_1 = contentlog_component_1_1;
            }
        ],
        execute: function () {
            routes = [
                { path: 'organization', component: organization_component_1.OrganizationComponent },
                { path: 'eventsetup', component: eventsetup_component_1.EventSetupComponent },
                { path: 'dashboard', component: dashboard_component_1.DashboardComponent },
                { path: 'auditlog', component: audithistory_component_1.AuditHistoryComponent },
                { path: 'contentlog', component: contentlog_component_1.ContentLogComponent },
                { path: '**', redirectTo: 'dashboard' }
            ];
            exports_1("appRouting", appRouting = router_1.RouterModule.forRoot(routes, { useHash: true }));
        }
    };
});
//# sourceMappingURL=app.routing.js.map