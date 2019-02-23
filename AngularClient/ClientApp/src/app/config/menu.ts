// tslint:disable-next-line:no-shadowed-variable
import { ConfigModel } from '../core/interfaces/config';

// tslint:disable-next-line:no-shadowed-variable
export class MenuConfig implements ConfigModel {
	public config: any = {};

	constructor() {
		this.config = {
			header: {
				self: {},
				items: [
				]
			},
			aside: {
				self: {},
				items: [
					{
						title: 'Inicio',
						desc: 'Some description goes here',
						root: true,
						icon: 'flaticon-line-graph',
						page: '/', 
					},  
					{
						title: 'Administración',
						root: true, 
						icon: 'flaticon-interface-8',
						submenu: [
							{
								//permission: 'Admin.User.Administracion',
								title: 'Usuarios',
								icon: 'flaticon-user', 
								root: true, 
								page: '/admin/users',
								 
							} 
						]
					}
				]
			}
		};
	}
}
