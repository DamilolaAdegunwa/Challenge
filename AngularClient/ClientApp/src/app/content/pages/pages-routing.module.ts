import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PagesComponent } from './pages.component';
//import { ActionComponent } from './header/action/action.component';
import { NgxPermissionsGuard } from 'ngx-permissions';
//import { ProfileComponent } from './header/profile/profile.component';
import { ErrorPageComponent } from './snippets/error-page/error-page.component';
import { AuthGuard } from './auth/guards';
import { AnonymousGuard } from './auth/guards/anonymous.guard';

const routes: Routes = [
	{
		path: '',
		component: PagesComponent,
		canActivate: [AnonymousGuard], 
		children: [
			{
				path: '',
				loadChildren: './components/dashboard/dashboard.module#DashboardModule'
			}, 
			{
				path: 'admin',
				loadChildren: './components/admin/admin.module#AdminModule'
			}, 
		]
	},
	{
		path: '404',
		component: ErrorPageComponent
	},
	{
		path: 'error/:type',
		component: ErrorPageComponent
	},
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class PagesRoutingModule {}
