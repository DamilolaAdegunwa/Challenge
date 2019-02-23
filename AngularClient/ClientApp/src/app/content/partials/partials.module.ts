import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { QuickSidebarComponent } from './layout/quick-sidebar/quick-sidebar.component';
import { ScrollTopComponent } from './layout/scroll-top/scroll-top.component';
import { ListSettingsComponent } from './layout/quick-sidebar/list-settings/list-settings.component';
import { MessengerModule } from './layout/quick-sidebar/messenger/messenger.module';
import { CoreModule } from '../../core/core.module';
import { ListTimelineModule } from './layout/quick-sidebar/list-timeline/list-timeline.module';
import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PortletModule } from './content/general/portlet/portlet.module';
import { SpinnerButtonModule } from './content/general/spinner-button/spinner-button.module';

import { MatInputModule,
    MatSortModule,
    MatProgressSpinnerModule,
    MatTableModule,
    MatPaginatorModule,
    MatSelectModule,
    MatProgressBarModule,
    MatButtonModule,
    MatCheckboxModule,
    MatIconModule,
    MatTooltipModule} from '@angular/material';

@NgModule({
	declarations: [
		QuickSidebarComponent,
		ScrollTopComponent,
		//TooltipsComponent,
		ListSettingsComponent,
		//NoticeComponent,
		//BlogComponent,
		//FinanceStatsComponent,
		//PackagesComponent,
		//TasksComponent,
		//SupportTicketsComponent,
		//RecentActivitiesComponent,
		//RecentNotificationsComponent,
		//AuditLogComponent,
		//BestSellerComponent,
		//AuthorProfitComponent,
		//DataTableComponent,
		//StatComponent,
	],
	exports: [
		QuickSidebarComponent,
		ScrollTopComponent,
		//TooltipsComponent,
		ListSettingsComponent,
		//NoticeComponent,
		//BlogComponent,
		//FinanceStatsComponent,
		//PackagesComponent,
		//TasksComponent,
		//SupportTicketsComponent,
		//RecentActivitiesComponent,
		//RecentNotificationsComponent,
		//AuditLogComponent,
		//BestSellerComponent,
		//AuthorProfitComponent,
		//DataTableComponent,
		//StatComponent,

		PortletModule,
		SpinnerButtonModule
	],
	imports: [
		CommonModule,
		RouterModule,
		NgbModule,
		PerfectScrollbarModule,
		MessengerModule,
		ListTimelineModule,
		CoreModule,
		PortletModule,
		SpinnerButtonModule,
		MatSortModule,
		MatProgressSpinnerModule,
		MatTableModule,
		MatPaginatorModule,
		MatSelectModule,
		MatProgressBarModule,
		MatButtonModule,
		MatCheckboxModule,
		MatIconModule,
		MatTooltipModule,
		//WidgetChartsModule
	]
})
export class PartialsModule {}
