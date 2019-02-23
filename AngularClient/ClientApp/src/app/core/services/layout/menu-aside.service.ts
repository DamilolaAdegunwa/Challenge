import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { MenuConfigService } from '../menu-config.service';
import { ClassInitService } from '../class-init.service';
import * as objectPath from 'object-path';
import { LayoutConfigService } from '../layout-config.service';
import { PermissionCheckerService } from '../../shared/permission-checker.service';
import { debounce } from 'rxjs/operators';
import { async } from '@angular/core/testing';

@Injectable()
export class MenuAsideService {
	classes: string;
	menuList$: BehaviorSubject<any[]> = new BehaviorSubject([]);

	isDropdown: number = 0;
	dropdownTimeout: number;
	isScrollable: number = 0;

	constructor(
		private menuConfigService: MenuConfigService,
		private classInitService: ClassInitService,
		private layoutConfigService: LayoutConfigService,
		private permission: PermissionCheckerService
	) {
		// get menu list
		this.menuConfigService.onMenuUpdated$.subscribe(model => {
			setTimeout(() => {
				var m = objectPath.get(model.config, 'aside.items');
				this.clearMenuitem(m);
				this.menuList$.next(m);
			});
		});

		this.layoutConfigService.onLayoutConfigUpdated$.subscribe(config => {
			if (objectPath.get(config, 'config.aside.left.fixed')) {
				this.isScrollable = 1;
				this.isDropdown = 0;
			}

			// tslint:disable-next-line:max-line-length
			if (!objectPath.get(config, 'config.aside.left.fixed') && !objectPath.get(config, 'config.menu.aside.desktop_and_mobile.submenu.accordion')) {
				this.isScrollable = 0;
				this.isDropdown = 1;
				this.dropdownTimeout = objectPath.get(config, 'config.menu.aside.desktop_and_mobile.submenu.dropdown.hover_timeout');
			}
		}); 

	}


	private clearMenuitem(items: any) {

		items.forEach((item, index) => {

			item.granted = true;

			if (item.permission) { 
				item.granted = this.permission.isGranted(item.permission); 
			}

			if (item.submenu && item.submenu.length > 0) {
				this.clearMenuitem(item.submenu);
			}

			if (!item.permission && !item.route) {
				if (item.submenu) { 
					if (!item.submenu.some(x => x.granted)) {
						item.granted = false;	
					}					
				}
			}

		});
	}


	//showMenuItem(permissionName): boolean {

	//	if (permissionName) {
	//		return this.permission.isGranted(permissionName);
	//	}

		

	//	return true;
	//}



}
