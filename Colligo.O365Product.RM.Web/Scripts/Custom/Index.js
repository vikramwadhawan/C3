
$(document).ready(function () {
    console.log('resetting storage...');
    cleanStorage();    
    window.localStorage.setItem("rmo_auth_token", idToken);
    window.localStorage.setItem("tenant_url", tenantUrl);
});

function cleanStorage() {
    window.localStorage.removeItem("rmo_auth_token");    
    window.localStorage.removeItem("rmo_node_detail");
    window.localStorage.removeItem("rmo_event_setting");
    window.localStorage.removeItem("rmo_eventReport");
    window.localStorage.removeItem("rmo_eventReportSearch");  
    window.localStorage.removeItem("rmo_errorLogs"); 
    window.localStorage.removeItem("rmo_eventSummaryData"); 
    window.localStorage.removeItem("rmo_errorLogs_pagenum");    
    window.localStorage.removeItem("rmo_itemProcessedToday");
    window.localStorage.removeItem("rmo_itemProcessedToday_pagenum");    
    window.localStorage.removeItem("rmo_event_mappedCount");    
}
