import { Component, ComponentFactoryResolver, ViewContainerRef, OnInit, Output, Input, ViewEncapsulation, EventEmitter, Type, ViewChild, ReflectiveInjector, Inject, Injector, OnDestroy, AfterViewInit, ElementRef, ChangeDetectorRef } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
//import { DialogService, DialogComponent } from 'ng2-bootstrap-modal';
//import { MatPaginator, MatSort } from '@angular/material';
//import { Page } from 'app/shared/models/pagination';
//import { DataSource } from '@angular/cdk/collections';
//import { AlertService } from 'app/shared/components/alert/alert.service';
//import { LocatorService } from 'app/shared/services/locator.service';
//import { ICRUDComponent, DTO, Data, CrudDataSource, FilterDTO } from 'app/shared/components/crud/crud.model';
//import { CrudService } from 'app/shared/components/crud/crud.service';
//import { ITEMS_PER_PAGE } from 'app/shared/constants/constants';
//import { ConfirmComponent } from 'app/shared/components/confirm/confirm.component';
//import { AppComponentBase } from '../../shared/common/app-component-base';
import { FilterDTO, Dto, IDto, ResponseModel, PaginListResultDto, ADto, ViewMode, ItemDto } from '../../shared/model/base.model';
//import { LazyLoadEvent } from 'primeng/components/common/lazyloadevent';

//import { DataTable } from 'primeng/components/datatable/datatable';
//import { Paginator } from 'primeng/components/paginator/paginator';
//import { LazyLoadEventData } from '../helpers/PrimengDatatableHelper';

//import { ModalDirective, BsModalService } from 'ngx-bootstrap/modal';

import { DetailComponent, IDetailComponent } from './detail.component';
import { CrudService } from '../services/crud.service';
import { AppComponentBase } from '../app-component-base';
import { debug } from 'util';
import { SelectionModel } from '@angular/cdk/collections';
import { MatPaginator, MatSort } from '@angular/material';
import { merge, of, fromEvent } from 'rxjs';
import { tap, catchError, finalize, debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { SubheaderService } from '../../services/layout/subheader.service';
import { QueryParamsModel } from '../../models/query-params.model';
import { QueryResultsModel } from '../../models/query-results.model';
import { LayoutUtilsService } from '../../utils/layout-utils.service';
import { BaseDataSource } from '../models/data-sources/_base.datasource';

//import { DialogService, DialogComponent } from 'ng2-bootstrap-modal';
//import { LocatorService } from '../../shared/common/services/locator.service';

//import { BreadcrumbsService } from '../../theme/layouts/breadcrumbs/breadcrumbs.service';



export class CrudDataSource extends BaseDataSource {
	constructor() {
		super();
	}

	//loadProducts(queryParams: QueryParamsModel) {


	//	var filter = new UserFilter();
	//	filter.PageSize = queryParams.pageSize | 10;
	//	filter.Page = queryParams.pageNumber | 10;
	//	//filter.Sort = queryParams.
	//	//filter.FilterText = queryParams.filter;

	//	this.loadingSubject.next(true);
	//	this.productsService.lastFilter$.next(queryParams);

	//	this.productsService.search(filter).pipe(
	//		tap(res => {
	//			this.entitySubject.next(res.DataObject.Items);
	//			this.paginatorTotalSubject.next(res.DataObject.TotalCount);
	//		}),
	//		catchError(err => of(new QueryResultsModel([], err))),
	//		finalize(() => this.loadingSubject.next(false))
	//	).subscribe();

	//	//this.productsService.lastFilter$.next(queryParams);
	//	//this.loadingSubject.next(true);


	//}
}




export abstract class BaseCrudComponent<T extends ADto, F extends FilterDTO> extends AppComponentBase implements ICRUDComponent, OnInit, OnDestroy, AfterViewInit {


	protected cfr: ComponentFactoryResolver; 
	filter: F;
	public isTableLoading = false;
	advancedFiltersAreShown = false;
	list: T[] = [];

	icon: string;
	title: string;
	moduleName: string;

 
	allowAdd: boolean = true;
	allowDelete: boolean = true;
	allowModify: boolean = true; 

	active = true;
	protected detailElement: IDetailComponent; 

	dataSource: CrudDataSource;
	@ViewChild(MatPaginator) paginator: MatPaginator;
	@ViewChild(MatSort) sort: MatSort;
	// Filter fields
	@ViewChild('searchInput') searchInput: ElementRef;
	filterStatus: string = '';
	filterCondition: string = '';
	// Selection
	selection = new SelectionModel<T>(true, []);

	subheaderService: SubheaderService;
	layoutUtilsService: LayoutUtilsService;


	@Output() modalSave: EventEmitter<any> = new EventEmitter<any>();

	//public breadcrumbsService: BreadcrumbsService;
	protected router: Router;
	protected route: ActivatedRoute;
	constructor(
		protected service: CrudService<T>,
		injector: Injector
	) {
		super(injector);
		  

		this.cfr = injector.get(ComponentFactoryResolver);


		this.subheaderService = injector.get(SubheaderService);
		this.layoutUtilsService = injector.get(LayoutUtilsService);

		  
		this.filter = this.getNewfilter();

		this.router = injector.get(Router);

		this.route = injector.get(ActivatedRoute);

		this.subheaderService.setTitle(this.title);
	}

	SetAllowPermission() { 
		 
	}

	getRangeLabel(page: number, pageSize: number, length: number): string {
		if (length === 0 || pageSize === 0) {
			return `0 de ${length}`;
		}
		length = Math.max(length, 0);
		const startIndex = page * pageSize;
		// If the start index exceeds the list length, do not try and fix the end index to the end.
		const endIndex = startIndex < length ?
			Math.min(startIndex + pageSize, length) :
			startIndex + pageSize;
		return `${startIndex + 1} - ${endIndex} de ${length}`;
	}



	ngOnInit() {

		this.paginator._intl.itemsPerPageLabel = 'Registros por página';
		this.paginator._intl.firstPageLabel = 'Primera página';
		this.paginator._intl.previousPageLabel = 'Página anterior';
		this.paginator._intl.nextPageLabel = 'Página siguiente';
		this.paginator._intl.lastPageLabel = 'Última página';
		this.paginator._intl.getRangeLabel = this.getRangeLabel;


		this.SetAllowPermission();
		// If the user changes the sort order, reset back to the first page.
		this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

		/* Data load will be triggered in two cases:
		- when a pagination event occurs => this.paginator.page
		- when a sort event occurs => this.sort.sortChange
		**/
		merge(this.sort.sortChange, this.paginator.page)
			.pipe(
				tap(() => {
					this.onSearch();
				})
			)
			.subscribe();

		// Filtration, bind to searchInput

		if (this.searchInput) {
			fromEvent(this.searchInput.nativeElement, 'keyup')
				.pipe(
					debounceTime(250),
					distinctUntilChanged(),
					tap(() => {
						this.paginator.pageIndex = 0;
						this.onSearch();
					})
				)
				.subscribe();
		}

		
		// Set title to page breadCrumbs
		this.subheaderService.setTitle(this.title);
		// Init DataSource
		this.dataSource = new CrudDataSource();
 

		this.dataSource.entitySubject.subscribe(res => this.list = res);


		this.onSearch();
	}


	ngAfterViewInit() {

	 

	}

	ngOnDestroy(): void {

	}

	onView(row: any) {
		this.active = false;
		this.router.navigate(['./view'], { queryParams: { id: row.id }, relativeTo: this.route });
	}

	onEdit(row: T) {
		this.onEditID(row.Id);
	}

	onEditID(id: any) {
		if (!this.allowModify) {
			return;
		}
		this.active = false;
		this.router.navigate(['./edit'], { queryParams: { id: id }, relativeTo: this.route }); 
	}

	CloseChild(): void { 
	}



	onCreate() {
		if (!this.allowAdd) {
			return;
		}

		var url = this.router.url;
 
		this.router.navigate(['./add'], { relativeTo: this.route });

	}

	onDelete(item: T) {


		if (!this.allowDelete) {
			return;
		}

		var stringdto = this.getDescription(item);

		this.message.confirm('¿Está seguro de que desea eliminar el registro?', stringdto || 'Confirmación', (a) => {
			if (a.value) {
				this.service.delete(item.Id)
					.subscribe(() => {
						this.onSearch();
						this.notify.success(this.l('Registro eliminado correctamente'));
					});
			}

		});

	}

	getDescription(item: T): string {
		return '';
	}

	exportToExcel() {
	}


	/** SELECTION */
	isAllSelected() {
		const numSelected = this.selection.selected.length;
		const numRows = this.list.length;
		return numSelected === numRows;
	}

	/** Selects all rows if they are not all selected; otherwise clear selection. */
	masterToggle() {
		if (this.isAllSelected()) {
			this.selection.clear();
		} else {
			this.list.forEach(row => this.selection.select(row));
		}
	}



	onSearch() {
		this.selection.clear();

		const queryParams = new QueryParamsModel(
			this.filterConfiguration(),
			this.sort.direction,
			this.sort.active,
			this.paginator.pageIndex,
			this.paginator.pageSize
		);
		this.Search(queryParams); 
	}


	Search(queryParams: QueryParamsModel) {
		 

		this.filter.PageSize = this.paginator.pageSize || 10;
		this.filter.Page = this.paginator.pageIndex + 1; 

		if (queryParams.sortField) {
			this.filter.Sort = queryParams.sortField + ' ' + queryParams.sortOrder;
		}



		this.dataSource.loadingSubject.next(true);
		this.service.lastFilter$.next(queryParams);

		this.service.requestAllByFilter(this.filter).pipe(
			tap(res => {
				this.dataSource.entitySubject.next(res.DataObject.Items);
				this.dataSource.paginatorTotalSubject.next(res.DataObject.TotalCount);
			}),
			catchError(err => of(new QueryResultsModel([], err))),
			finalize(() => this.dataSource.loadingSubject.next(false))
		).subscribe();



	}


	filterConfiguration(): any {
		const filter: any = {}; 
		return filter;
	} 

	getNewfilter(): F {
		return null;
	} 

	reloadTable() {

	}

}

export abstract class CrudComponent<T extends ADto> extends BaseCrudComponent<T, FilterDTO>
{

	constructor(
		protected service: CrudService<T>,
		injector: Injector
	) {
		super(service, injector)
	}

	getNewfilter(): FilterDTO {
		return new FilterDTO();
	}

}

export interface ICRUDComponent {
	onView(row: any);
	onCreate(row: any);
	onEdit(row: any);
	onDelete(row: any);
	onSearch();
	reloadTable();
}
