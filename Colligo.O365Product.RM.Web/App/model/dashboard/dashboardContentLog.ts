export class DashboardContentLog {
    public id: number;
    public rowNumber?: number;
    public contentLogId: number;
    public contentType: string;
    public docId: string;
    public lastModifiedTime: Date;
    public fileType: string;
    public title: string;
    public eventColumnValue?: Date;
    public complianceTag: string;
    public complianceTagId: string;
    public complainceAssetId: string;
    public docUrl: string;
    public libraryName: string;
    public createdOn: Date;
    public modifiedOn?: Date;
    public documentName: string;
    public errorLog: string;

    constructor() { }
}