System.register([], function (exports_1, context_1) {
    "use strict";
    var MessageType, CONFIGURATION;
    var __moduleName = context_1 && context_1.id;
    return {
        setters: [],
        execute: function () {
            (function (MessageType) {
                MessageType[MessageType["Error"] = 0] = "Error";
                MessageType[MessageType["Notify"] = 1] = "Notify";
            })(MessageType || (MessageType = {}));
            exports_1("MessageType", MessageType);
            exports_1("CONFIGURATION", CONFIGURATION = {
                baseUrls: {
                    server: 'https://localhost:44317/'
                },
                colligoRMOApp: {
                    rmoVersion: window["config"].updraftVersion,
                    displayPageSize: window["config"].displayPageSize,
                    getSharePointRefreshToken: window["config"].getSharePointRefreshToken,
                    getAllEventSetupByOrgUrl: window["config"].getAllEventSetupByOrgUrl,
                    saveEventSetup: window["config"].saveEventSetup,
                    updateEventSetup: window["config"].updateEventSetup,
                    getAllContentLog: window["config"].getAllContentLog,
                    getAllFieldsByContentId: window["config"].getAllFieldsByContentId,
                    searchSharepointSite: window["config"].searchSharepointSite,
                    saveEventSettingUser: window["config"].saveEventSettingUser,
                    getEventSettingUser: window["config"].getEventSettingUser,
                    searchEventReport: window["config"].searchEventReport,
                    getAllDashboardContentLog: window["config"].getAllDashboardContentLog,
                    getEventSummary: window["config"].getEventSummary,
                    getContentAuditSummary: window["config"].getContentAuditSummary,
                    getTimeZone: window["config"].getTimeZone,
                    getEventSettingCount: window["config"].getEventSettingCount
                },
                localStorage: {
                    keys: {
                        TIMEZONE: 'timezone',
                        Node_Detail: 'rmo_node_detail',
                        Tenant_Url: 'tenant_url',
                        SHAREPOINT_TOKEN: 'rmo_sp_token',
                        AUTH_TOKEN: 'rmo_auth_token',
                        EventSettings: 'rmo_event_setting',
                        EventMappedCount: 'rmo_event_mappedCount',
                        CurrentLoginUserId: 'userId',
                        OrganizationId: 'orgId',
                        EventReport: 'rmo_eventReport',
                        EventReportSearch: 'rmo_eventReportSearch',
                        ErrorLogs: 'rmo_errorLogs',
                        ErrorLogsPageNumber: 'rmo_errorLogs_pagenum',
                        ItemProcessedToday: 'rmo_itemProcessedToday',
                        ItemProcessedTodayPageNumber: 'rmo_itemProcessedToday_pagenum',
                        EventSummaryData: 'rmo_eventSummaryData',
                    }
                },
                constants: {
                    messages: {
                        Technical_Error: 'Technical issue. Try again.',
                        REQUEST_OBJECT_MISSING: 'Request object is missing',
                        SP_TOKEN_MISSING: 'Sharepoint token is missing',
                        SAVED_SUCCESSFULLY: 'Saved Successfully',
                        Event_Setting: {
                            Invalid_Data: 'Data is invalid',
                            Duplicate_Event: 'Event alreday exist'
                        }
                    },
                    calendar: {
                        Month: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
                    },
                    MSG_TIMEOUT: 5000
                }
            });
        }
    };
});
//# sourceMappingURL=app.constant.js.map