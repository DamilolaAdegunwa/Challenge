import { ChangeDetectionStrategy, Component, OnInit, ViewChild, Input } from '@angular/core';
import { Router } from '@angular/router';
import { LayoutConfigService } from '../../../../core/services/layout-config.service';
import { SubheaderService } from '../../../../core/services/layout/subheader.service';

@Component({
	selector: 'm-dashboard',
	templateUrl: './dashboard.component.html',
	changeDetection: ChangeDetectionStrategy.OnPush
})
export class DashboardComponent implements OnInit {

	public config: any;

	constructor(
		private router: Router,
		private configService: LayoutConfigService,
		private subheaderService: SubheaderService
	) {
		this.subheaderService.setTitle('Inicio');

		//fromEvent(this.sliderElement.onFinish 'keyup')
		//	.pipe(
		//		debounceTime(250),
		//		distinctUntilChanged(),
		//		tap(() => {
		//			this.paginator.pageIndex = 0;
		//			this.onSearch();
		//		})
		//	)
		//	.subscribe();
	}


	ngOnInit(): void { }

}
