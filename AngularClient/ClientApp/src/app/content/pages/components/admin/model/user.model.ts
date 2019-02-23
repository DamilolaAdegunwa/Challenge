 import { Dto, FilterDTO } from '../../../../../core/shared/model/base.model';

export class User extends Dto<string> {
    getDescription(): string {
		return this.UserName;
    }

    constructor(data?: any) {
        super(data);
    }

	Gender: string
	FirstName: string
	LastName: string
	Email: string
	BirthDate: Date
	Uuid: string
	UserName: string
	Location: Location 
 
}

export class Location extends Dto<string> {
	getDescription(): string { 
		return this.State;
	}

	constructor(data?: any) {
		super(data);
	}

	State: string
	Street: string
	City: string
	PostCode: string  
}




export class UserFilter extends FilterDTO {  
}


 



