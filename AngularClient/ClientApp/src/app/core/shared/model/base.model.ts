import * as moment from 'moment';

export interface SearchDTO { }


export class IFilterDTO {
	IdValue: any;
    Page: number;
    PageSize: number;
    Sort: String;
    FilterText: String;
}

export class FilterDTO {
	IdValue: any;
    Page: number;
    PageSize: number;
    Sort: String;
    FilterText: String;

    constructor() {
    }
}

export interface IDto {
	IdValue: any
    getDescription(): string;
}


export interface Data {
    isReadOnly: boolean;
	IdValue: number;
}

export abstract class ADto implements IDto {
    abstract getDescription(): string;
	IdValue: any
    constructor(data?: any) {

        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }
}

export abstract class Dto<T> extends ADto {
    abstract getDescription(): string;
	IdValue: T
    Description: string

    constructor(data?: any) {
        super(data);

    }
}


export class ItemDto extends Dto<number> {


    getDescription(): string {
        return this.Description;
    }
	IdValue: number;
    Description: string;
    IsSelected: boolean;
    animate: boolean;


    constructor(data?: any) {
        super(data);
        this.animate = false;
    }
}


export class GroupItemDto extends Dto<number> {


    getDescription(): string {
        return this.Description;
    }
	IdValue: number;
    Description: string;
    Items: ItemDto[];
    constructor(data?: any) {
        super(data);
        this.Items = [];
    }
}




export class PaginListResultDto<T>  {
    Items: T[];
    TotalCount: number;
}




export class PagedRequestDto {
    TotalCount: number;
}



export class ResponseModel<T>
{
    Messages: string[] = [];
    //TODO: poner en enum
    //status: Result;
    Status: StatusResponse;
    DataObject: T;
}





export class UserFilter extends FilterDTO {

}




export enum Result {
    Ok,
    Default
}

export enum ViewMode {
    Undefined,
    Add,
    Modify,
    Delete,
    List,
    View
}




export enum StatusResponse {
    Ok = "Ok",
    Fail = "Fail",
    Other = "Other"
}
