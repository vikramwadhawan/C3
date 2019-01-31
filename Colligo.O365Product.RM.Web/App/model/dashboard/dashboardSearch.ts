export class DashboardSearch {
    public organizationUrl: string;
    public startDate?: Date;
    public endDate?: Date;
    public status: string;
    public searchTerm: string;    
    public errorRecord: boolean;
    public pageNumber: number;
    public pageSize: number;
    constructor() { }
}