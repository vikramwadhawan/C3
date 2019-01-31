import { Field } from "./fieldModel";

export class ContentType {
    public id: string;
    public name: string;
    public selected: boolean;
    public contentColumns: Array<Field>;
}