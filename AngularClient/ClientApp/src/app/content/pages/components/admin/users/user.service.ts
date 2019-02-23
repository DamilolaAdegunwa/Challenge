import { Injectable } from '@angular/core';
import { Response } from '@angular/http';
import { CrudService } from '../../../../../core/shared/services/crud.service';


import { HttpClient, HttpHeaders } from '@angular/common/http';


//import 'rxjs/Rx';
//import 'rxjs/add/observable/throw';
//import 'rxjs/add/operator/map';
import * as moment from 'moment';
import { environment } from '../../../../../../environments/environment';
//import { AuthService } from '../../../../auth/auth.service';
import { UserDto, ListResultDtoOfUserListDto, ResetPasswordInput, UserRoleDto } from '../model/user.model';
import { GetPermissionsForEditOutput, UpdatePermissionsInput } from '../model/permission.model';
import { Observable } from 'rxjs';
import { ResponseModel } from '../../../../../core/shared/model/base.model';

@Injectable()
export class UserService extends CrudService<UserDto> {
	 
    private identityUrl: string = '';
    constructor(
        protected http: HttpClient) {
        super(http);

		this.identityUrl = environment.apiUrl + '/User';
        this.endpoint = this.identityUrl;

    } 


}

