export class EventSetting {
    public eventSettingId: number;
    public organizationMasterId: number;
    public contentType: string;
    public contentTypeId: string;
    public eventColumnName: string;
    public eventColumnInternalName: string
    public isActive: boolean;
    public comments: string;
    public createdBy: number;
    public createdOn: Date;
    public lastRetrivalTime?: Date;
    public contentSiteUrl;
    constructor() { }
}