import { ConfigModel } from '../core/interfaces/config';

export class PagesConfig implements ConfigModel {
	public config: any = {};

	constructor() {
		this.config = {
			'/': {
				page: {
					title: 'Dashboard',
					desc: 'Latest updates and statistic charts'
				}
			}, 
			admin: {
				users: {
					page: { title: 'Users', desc: '' }
				}
			},
 
			404: {
				page: { title: '404 Not Found', desc: '', subheader: false }
			}
		};
	}
}
