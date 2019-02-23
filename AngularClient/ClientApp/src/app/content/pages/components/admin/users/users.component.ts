import { Component, OnInit, ViewEncapsulation,  Injector } from '@angular/core'; 
import { BaseCrudComponent } from '../../../../../core/shared/manager/crud.component';
import { UserService } from './user.service';   
import { User, UserFilter } from '../model/user.model';  
import { MatDialog } from '@angular/material';  

@Component({
    templateUrl: "./users.component.html",
    encapsulation: ViewEncapsulation.None,
})
export class UsersComponent extends BaseCrudComponent<User, UserFilter> implements OnInit {
	 
	 
	displayedColumns = ['Gender', 'FirstName', 'LastName', 'Email', 'BirthDate', 'UserName' , 'Uuid'];
	 
	constructor(injector: Injector,
		protected _userService: UserService, 
		public dialog: MatDialog
	) {
        super(_userService, injector);
        
        this.title = "Usuarios"
        this.moduleName = "Administración General";
        this.icon = "flaticon-users";        
    }
	 

	getNewfilter(): UserFilter {
		var f = new UserFilter(); 
		return f;
    } 

	getNewItem(item: User): User {
		return new User(item);
    }


	getDescription(item: User): string {
		return item.Description;
    }

	  

    
	/* UI */
	getItemActiveString(active: boolean = false): string {  
		if (active) {
			return 'Activo';
		}
		else {
			return 'Inactivo';
		} 
	}

	getItemCssClassByActive(active: boolean = false): string {
		if (active) {
			return 'success';
		}
		else {
			return 'metal';
		}  
	}

	getItemCssClassByAssignedRoles(active: number): string {
		if (active && active != 0) {
			return 'warning';
		}
		else {
			return 'success';
		}
	}



}
