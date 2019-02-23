

import * as moment from 'moment';
import { Dto, FilterDTO, PaginListResultDto } from '../../../../../core/shared/model/base.model';

export class UserDto extends Dto<number> {
    getDescription(): string {
		return this.Name;
    }

    constructor(data?: any) {
        super(data);
    }

	Name: string;
	Email: string;
	DisplayName: string; 
	Password: string;
	ConfirmPassword: string;
	UserRoles: UserRoleDto[];
	IsActive: boolean;
	
	AssignedRoles: number
	AssignedRolesText: string
}

export class UserRoleDto {
    RoleId: number;
    RoleName: string;
    RoleDisplayName: string;
    IsAssigned: boolean;
}



export class UserFilter extends FilterDTO {
    Id: number;
    Page: number;
    PageSize: number;
    Sort: String;
    RoleId: number
}





export class ListResultDtoOfUserListDto extends PaginListResultDto<UserDto> implements IListResultDtoOfUserListDto {


}

export interface IListResultDtoOfUserListDto {
    Items: UserDto[];
    TotalCount: number;
}


export class ResetPasswordInput implements IResetPasswordInput {
	UserId: number;
	Password: string;
	ConfirmPassword: string;
}

export interface IResetPasswordInput {
	UserId: number;
	Password: string;
	ConfirmPassword: string;
}



