import { Injectable } from '@angular/core'; 
import { CrudService } from '../../../../../core/shared/services/crud.service'; 
import { HttpClient } from '@angular/common/http'; 
import { environment } from '../../../../../../environments/environment'; 
import { User } from '../model/user.model';  

@Injectable()
export class UserService extends CrudService<User> {
	 
    private identityUrl: string = '';
    constructor(
        protected http: HttpClient) {
        super(http);

		this.identityUrl = environment.apiUrl + '/User';
        this.endpoint = this.identityUrl;

    } 


}

