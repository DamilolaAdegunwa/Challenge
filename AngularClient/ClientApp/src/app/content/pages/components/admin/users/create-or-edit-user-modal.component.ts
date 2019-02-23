import { Component, Injector, ChangeDetectionStrategy } from '@angular/core'; 
import { DetailEmbeddedComponent } from '../../../../../core/shared/manager/detail.component';
import { UserService } from './user.service'; 
import * as _ from 'lodash'; 
import { User } from '../model/user.model';
import { ViewMode } from '../../../../../core/shared/model/base.model';

@Component({
	selector: 'createOrEditUserModal',
	templateUrl: './create-or-edit-user-modal.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class CreateOrEditUserModalComponent extends DetailEmbeddedComponent<User> {

	term: { isAssigned: true }; 
	selectedTab: number = 0; 
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
	
	getNewItem(item: User): User {
		return new User()
	}
	
	completedataBeforeShow(item: User): any {

		if (this.viewMode == ViewMode.Add) {
			 
		}
		else {
 
		}
	}

	completedataBeforeSave(item: User): any { 
	}
 
 

}
