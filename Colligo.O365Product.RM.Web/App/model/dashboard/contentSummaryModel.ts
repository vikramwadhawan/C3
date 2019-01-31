export class ContentSummaryModel {
    public id: number;
    public rowNumber?: number;
    public contentLogId: number;
    public contentType: string;
    public docId: string;
    public lastModifiedTime: Date;
    public fileType: string;
    public eventColumnValue?: Date;
    public complianceTag: string;
    public complianceTagId: string;
    public complainceAssetId: string;
    public docUrl: string;
    public documentName: string;
    public createdOn: Date;
    public modifiedOn?: Date;
    public processStatus: string;
    public eventCreatedDate?: Date;
    public processDescription: string;
    constructor() { }
}