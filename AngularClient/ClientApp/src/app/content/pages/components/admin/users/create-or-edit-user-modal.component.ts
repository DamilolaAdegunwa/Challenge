import { Component, Injector, ChangeDetectionStrategy } from '@angular/core'; 
import { DetailEmbeddedComponent } from '../../../../../core/shared/manager/detail.component';
import { UserService } from './user.service'; 
import * as _ from 'lodash'; 
import { UserRoleDto, UserDto } from '../model/user.model';
import { ViewMode } from '../../../../../core/shared/model/base.model';

@Component({
	selector: 'createOrEditUserModal',
	templateUrl: './create-or-edit-user-modal.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class CreateOrEditUserModalComponent extends DetailEmbeddedComponent<UserDto> {

	term: { isAssigned: true };
	roles: UserRoleDto[];
	selectedTab: number = 0;
	isNew: boolean = false;
	constructor(
		injector: Injector,
		protected service: UserService) {
		super(service, injector);
		this.title = "Usuarios";
		this.page = 'users';
		this.moduleAction = "admin";
		this.moduleName = "Administración";
		this.icon = "flaticon-users";
	}


	ngAfterViewChecked(): void { 
	}
	
	getNewItem(item: UserDto): UserDto {
		return new UserDto()
	}
	
	completedataBeforeShow(item: UserDto): any {

		if (this.viewMode == ViewMode.Add) {
			 
		}
		else {
 
		}
	}

	completedataBeforeSave(item: UserDto): any {
		this.detail.UserRoles = this.roles;
	}
 
 

}
