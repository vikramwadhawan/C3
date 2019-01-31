import { ContentType } from "./eventSetup/contentTypeModel";
import { EventSetting } from "./eventSetting";
export class SharepointRequest {
    public url: string;
    public accessToken: string;
    public idToken: string;
    public serverRelativeUrl: string;
    public type: string;
    public name: string;
    public folderName: string;
    public searchTerm: string;
    public contentTypeId: string;
    public fieldId: string;
    public pageSize: number;
    public pageNo: number;
    public referer: string;
    public folderExist: boolean;
    public clientTimeZone: string;
    public pagingInfo: string;
    public contentType: Array<ContentType>;
    public eventSetting: Array<EventSetting>;
}
