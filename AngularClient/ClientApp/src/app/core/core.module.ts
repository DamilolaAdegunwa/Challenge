import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MenuAsideDirective } from './directives/menu-aside.directive';
import { MenuAsideOffcanvasDirective } from './directives/menu-aside-offcanvas.directive';
import { MenuHorizontalOffcanvasDirective } from './directives/menu-horizontal-offcanvas.directive';
import { MenuHorizontalDirective } from './directives/menu-horizontal.directive';
import { ClipboardDirective } from './directives/clipboard.directive';
import { ScrollTopDirective } from './directives/scroll-top.directive';
import { HeaderDirective } from './directives/header.directive';
import { MenuAsideToggleDirective } from './directives/menu-aside-toggle.directive';
import { QuickSidebarOffcanvasDirective } from './directives/quick-sidebar-offcanvas.directive';
import { FirstLetterPipe } from './pipes/first-letter.pipe';
import { TimeElapsedPipe } from './pipes/time-elapsed.pipe';
import { QuickSearchDirective } from './directives/quick-search.directive';
import { JoinPipe } from './pipes/join.pipe';
import { GetObjectPipe } from './pipes/get-object.pipe';
import { ConsoleLogPipe } from './pipes/console-log.pipe';
import { SafePipe } from './pipes/safe.pipe';
import { PortletDirective } from './directives/portlet.directive';
import { LocalizationService } from './shared/localization.service';
import { PermissionCheckerService } from './shared/permission-checker.service';
import { LocalStorageService } from './shared/services/storage.service';
import { NotifyService } from './shared/notify.service';
import { ModalModule, TabsModule } from 'ngx-bootstrap';
import { AlertComponent } from './dialogs/alert/alert.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { GroupByPipe } from './pipes/core.pipe';
import { FetchEntityDialogComponent } from './dialogs/fetch-entity-dialog/fetch-entity-dialog.component';
import { UpdateStatusDialogComponent } from './dialogs/update-status-dialog/update-status-dialog.component';
import { DeleteEntityDialogComponent } from './dialogs/delete-entity-dialog/delete-entity-dialog.component';
import { ActionNotificationComponent } from './dialogs/action-natification/action-notification.component';
import { MatTooltipModule, MatTabsModule, MatSnackBarModule, MatProgressSpinnerModule, MatCheckboxModule, MatSortModule, MatPaginatorModule, MatCardModule, MatDatepickerModule, MatProgressBarModule, MatNativeDateModule, MatIconModule, MatRadioModule, MatAutocompleteModule, MatTableModule, MatInputModule, MatSelectModule, MatMenuModule, MatButtonModule, MatDialogModule } from '@angular/material';

@NgModule({
	imports: [FormsModule, CommonModule, ModalModule.forRoot(), TabsModule.forRoot(),

		MatButtonModule,
		MatSelectModule,
		MatInputModule,
		MatIconModule,
		MatProgressBarModule,
		MatProgressSpinnerModule,
		MatDialogModule,
		ReactiveFormsModule
	],
	declarations: [
		// directives
		MenuAsideDirective,
		MenuAsideOffcanvasDirective,
		MenuHorizontalOffcanvasDirective,
		MenuHorizontalDirective,
		ScrollTopDirective,
		HeaderDirective,
		MenuAsideToggleDirective,
		QuickSidebarOffcanvasDirective,
		QuickSearchDirective,
		ClipboardDirective,
		PortletDirective,
		// pipes
		FirstLetterPipe,
		TimeElapsedPipe,
		JoinPipe,
		GetObjectPipe,
		ConsoleLogPipe,
		SafePipe,
		GroupByPipe,
		ActionNotificationComponent,
		DeleteEntityDialogComponent,
		FetchEntityDialogComponent,
		UpdateStatusDialogComponent,
		AlertComponent
	],
	exports: [
		// directives
		MenuAsideDirective,
		MenuAsideOffcanvasDirective,
		MenuHorizontalOffcanvasDirective,
		MenuHorizontalDirective,
		ScrollTopDirective,
		HeaderDirective,
		MenuAsideToggleDirective,
		QuickSidebarOffcanvasDirective,
		QuickSearchDirective,
		ClipboardDirective,
		PortletDirective,
		// pipes
		FirstLetterPipe,
		TimeElapsedPipe,
		JoinPipe,
		GetObjectPipe,
		ConsoleLogPipe,
		SafePipe,
		ModalModule,
		TabsModule,
		GroupByPipe
	],
	entryComponents: [
		ActionNotificationComponent,
		DeleteEntityDialogComponent,
		FetchEntityDialogComponent,
		UpdateStatusDialogComponent
	],
	//providers: [LocalizationService, LocalStorageService, PermissionCheckerService, NotifyService]
})
export class CoreModule {}
