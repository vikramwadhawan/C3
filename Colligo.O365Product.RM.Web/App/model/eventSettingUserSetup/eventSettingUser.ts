import { AgentTimeZoneModel } from "./agentTimeZoneModel";


export class EventSettingUser {

    public siteUrl: string;
    public spUserId: string;
    public spUserPassword: string;   
    public adgUserId: string;
    public adgUserPassword: string;
    public sameAsOffice: boolean;
    public agentTimeZoneModel: AgentTimeZoneModel;
    constructor() { }
  
}