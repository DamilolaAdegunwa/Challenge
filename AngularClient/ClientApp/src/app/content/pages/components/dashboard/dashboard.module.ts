import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import { RouterModule } from '@angular/router';
import { LayoutModule } from '../../../layout/layout.module';
import { PartialsModule } from '../../../partials/partials.module';
import { ListTimelineModule } from '../../../partials/layout/quick-sidebar/list-timeline/list-timeline.module';
import { IonRangeSliderModule } from "ng2-ion-range-slider";
import { FormsModule } from '@angular/forms';
import { CoreModule } from '../../../../core/core.module';

@NgModule({
	imports: [
		FormsModule,
		CommonModule,
		CoreModule,
		CommonModule,
		LayoutModule,
		PartialsModule,
		ListTimelineModule,
		IonRangeSliderModule,
		RouterModule.forChild([
			{
				path: '',
				component: DashboardComponent
			}
		])
	],
	providers: [],
	declarations: [DashboardComponent]
})
export class DashboardModule {}
