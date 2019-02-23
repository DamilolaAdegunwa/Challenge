import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router'; 
import { AdminComponent } from './admin.component'; 
import { PartialsModule } from '../../../partials/partials.module';

import {
	MatIconRegistry,
	MatIcon,
	MatInputModule,
	MatDatepickerModule,
	MatFormFieldModule,
	MatAutocompleteModule,
	MatSliderModule,
	MatListModule,
	MatCardModule,
	MatSelectModule,
	MatButtonModule,
	MatIconModule,
	MatNativeDateModule,
	MatSlideToggleModule,
	MatCheckboxModule,
	MatMenuModule,
	MatTabsModule,
	MatTooltipModule,
	MatSidenavModule,
	MatProgressBarModule,
	MatProgressSpinnerModule,
	MatSnackBarModule,
	MatGridListModule,
	MatTableModule,
	MatExpansionModule,
	MatToolbarModule,
	MatSortModule,
	MatDividerModule,
	MatStepperModule,
	MatChipsModule,
	MatPaginatorModule,
	MatDialogModule,
	MatRadioModule,

    MatTreeModule
} from '@angular/material';
import { UsersComponent } from './users/users.component';
import { CreateOrEditUserModalComponent } from './users/create-or-edit-user-modal.component';
import { UserService } from './users/user.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CoreModule } from '../../../../core/core.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TranslateModule } from '@ngx-translate/core';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar'; 
import { InterceptService } from '../../../../core/utils/intercept.service';



const routes: Routes = [
	{
		path: '',
		component: AdminComponent ,
		children: [
			{
				path: 'users',
				component: UsersComponent
			},			
			{
				path: 'users/add',
				component: CreateOrEditUserModalComponent
			},
			{
				path: 'users/edit',
				component: CreateOrEditUserModalComponent 
			},			
			{
				path: 'users/edit/:id',
				component: CreateOrEditUserModalComponent
			}
		]
	}
];

@NgModule({
	imports: [
		FormsModule,
		CommonModule,
		CoreModule,
		RouterModule.forChild(routes),
		//MaterialPreviewModule,
		PartialsModule,
		CoreModule,
		FormsModule,
		ReactiveFormsModule,
		MatInputModule,
		MatFormFieldModule,
		MatDatepickerModule,
		MatAutocompleteModule,
		MatListModule,
		MatSliderModule,
		MatCardModule,
		MatSelectModule,
		MatButtonModule,
		MatIconModule,
		MatNativeDateModule,
		MatSlideToggleModule,
		MatCheckboxModule,
		MatMenuModule,
		MatTabsModule,
		MatTooltipModule,
		MatSidenavModule,
		MatProgressBarModule,
		MatProgressSpinnerModule,
		MatSnackBarModule,
		MatTableModule,
		MatGridListModule,
		MatToolbarModule,
		MatExpansionModule,
		MatDividerModule,
		MatSortModule,
		MatStepperModule,
		MatChipsModule,
		MatPaginatorModule,
		MatDialogModule,
		MatRadioModule,
		MatTreeModule,
		HttpClientModule,
		PartialsModule,
		RouterModule.forChild(routes),
		PerfectScrollbarModule,

	],
	exports: [RouterModule],
	entryComponents: [
		CreateOrEditUserModalComponent
	],
	providers: [UserService,

		InterceptService,
		{
			provide: HTTP_INTERCEPTORS,
			useClass: InterceptService,
			multi: true
		},

	],
	declarations: [
		AdminComponent,
		UsersComponent,
		CreateOrEditUserModalComponent 
	]
})
export class AdminModule { }
