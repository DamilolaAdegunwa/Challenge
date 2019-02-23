import { ChangeDetectorRef, Component, OnInit, AfterViewInit, AfterViewChecked, ElementRef, ViewChild, Injector, Input, Output, EventEmitter, Renderer, forwardRef, SimpleChanges, SimpleChange } from '@angular/core';
import { NgForm, FormGroup, ControlValueAccessor, NG_VALUE_ACCESSOR, FormControl } from '@angular/forms';

import { ADto, ItemDto } from '../model/base.model';
import { CrudService } from '../services/crud.service';
import { debounceTime, distinctUntilChanged, switchMap, finalize, map } from 'rxjs/operators';
import { Observable, fromEvent } from 'rxjs';

 
export abstract class AutoCompletBaseComponent implements OnInit, AfterViewInit, ControlValueAccessor {

	@ViewChild('autocomplete') autocompleteElement: ElementRef;

	selectItem: any;

	items: any[] = [];
	@Input() emptyText = '';
	@Input() hint = '';

	@Input() placeholder = 'Buscar...';
	@Input() field = 'Description';
	@Input() minLength = 1;
	@Input() isRequired: boolean = false;

	myControl = new FormControl();
	filteredOptions: Observable<any[]>;


	@Output() selectedItemChange: EventEmitter<any> = new EventEmitter<any>();
	@Output() optionSelected: EventEmitter<any> = new EventEmitter<any>();
	 

	//private _renderer: Renderer

	protected cdr: ChangeDetectorRef;

	IsDisabled = false;
	@Input() livesearch = true;
	onChange = (rating: any) => { };
	onTouched = () => {
	};
	isLoading = false;
	private innerValue: any = '';
	constructor(
		protected injector: Injector) {

		//this.optionSelected.subscribe(e => {
		//	
		//	e = e;
		//});

		//this.selectedItemChange.subscribe(e => {
		//	
		//	e = e;
		//});

		//this._renderer = injector.get(Renderer)
		this.cdr = injector.get(ChangeDetectorRef);

		this.filteredOptions = this.myControl.valueChanges
			.pipe(
				//startWith(null),
				debounceTime(200),
				distinctUntilChanged(),
			switchMap(val => {
				return this.filterItems(val || '')
				})
			);
		//fromEvent(this.autocompleteElement.nativeElement, 'keyup')
		//	.pipe(
		//		debounceTime(250),
		//		distinctUntilChanged(),
		//	switchMap(val => {
		//		return this.filterItems(val || '')
		//		})
		//	)
		//	.subscribe();



	}

	displayFn(item?: any): string | undefined {
		
		return item ? item.Name : undefined;
	}

	ngAfterViewInit(): void {

	}

	detectChanges(): void {

		try {

			if (this.cdr) {
				this.cdr.detectChanges();
			}
		} catch (e) {

		}
	}

	//get accessor
	get value(): any {
		return this.innerValue;
	};

	//set accessor including call the onchange callback
	set value(v: any) {
		if (v !== this.innerValue) {
			this.innerValue = v;
			this.onChange(v);
		}
	}

	writeValue(value: any): void {

		var self = this;
		if (value != this.innerValue) {
			this.innerValue = value;
			this.onChange(this.value);
		}
	}
	registerOnChange(fn: any): void {
		// throw new Error("Method not implemented.");
		this.onChange = fn;
	}
	registerOnTouched(fn: any): void {
		//throw new Error("Method not implemented.");
		this.onTouched = fn;

	}
	setDisabledState?(isDisabled: boolean): void {

		//throw new Error("Method not implemented.");
		this.IsDisabled = isDisabled;
	}

	ngOnInit(): void {

	}

	//sobrescrivir para filtro custom
	protected GetFilter(query: any): any {
		var f = {
			FilterText: query
		};

		return f;
	}

	filterItems(val: string): Observable<any[]> {
		return null;
	}

	Unselect(event) {

	}


	Clear(event) {
		//Hack. limpiar valor cuando se deseleciona el autocomplete
		this.value = null;
	}


}

export abstract class AutoCompleteComponent<T extends ADto> extends AutoCompletBaseComponent implements OnInit, AfterViewInit, ControlValueAccessor {

     
    constructor(
        protected service: CrudService<T>,
		protected injector: Injector) {
		super(injector)        
    }

    ngAfterViewInit(): void {

    }



	filterItems(val: string): Observable<any[]> {
		this.isLoading = true;
		this.cdr.detectChanges();

		return this.service.requestAllByFilter(this.GetFilter(val))
			.pipe(
				finalize(() => {
					this.isLoading = false;
					this.cdr.detectChanges();
				}),
				map(response => {

					return response.DataObject.Items;
				}
				));

	}

 

}


