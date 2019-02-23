import { Component, OnInit, ViewEncapsulation,  Injector } from '@angular/core'; 
import { BaseCrudComponent } from '../../../../../core/shared/manager/crud.component';
import { UserService } from './user.service';   
import { User, UserFilter } from '../model/user.model';  
import { MatDialog } from '@angular/material';  
import moment = require('moment');

@Component({
    templateUrl: "./users.component.html",
    encapsulation: ViewEncapsulation.None,
})
export class UsersComponent extends BaseCrudComponent<User, UserFilter> implements OnInit {
	 
	 
	displayedColumns = ['Gender', 'FirstName', 'LastName', 'Email', 'BirthDate', 'UserName', 'Uuid','Acciones'];
	 
	constructor(injector: Injector,
		protected _userService: UserService, 
		public dialog: MatDialog
	) {
        super(_userService, injector);
        
        this.title = "Usuarios"
        this.moduleName = "Administración General";
        this.icon = "flaticon-users";        
    }
	 
	beforeSearch(items: User[]): any {
		var order = Object.assign([], items).sort(function (a, b) {
			var keyA = a.BirthDate,
				keyB = b.BirthDate;
			// Compare the 2 dates
			if (keyA < keyB) return -1;
			if (keyA > keyB) return 1;
			return 0;
		});

		if (order && order.length > 0) {
			var older = order[0] as User;
			items.find(f => f.IdValue == older.IdValue).older = true;
		}

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

	  

   



}
