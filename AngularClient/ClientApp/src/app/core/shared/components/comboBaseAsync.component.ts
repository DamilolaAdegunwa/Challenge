import { Component, OnInit, AfterViewInit, AfterViewChecked, ElementRef, ViewChild, Injector, Input, Output, EventEmitter, Renderer, forwardRef, SimpleChanges, SimpleChange, ChangeDetectorRef, OnDestroy } from '@angular/core';
import { NgForm, FormGroup, ControlValueAccessor, NG_VALUE_ACCESSOR, FormControl } from '@angular/forms';
import { ADto } from '../model/base.model';
import { CrudService } from '../services/crud.service';
import { MatSelect, MatSelectChange } from '@angular/material';
import { ReplaySubject, Subject } from 'rxjs';
import { takeUntil, distinctUntilChanged, debounceTime } from 'rxjs/operators';

export abstract class ComboBoxBaseAsyncComponent implements OnInit, AfterViewInit, OnDestroy,   ControlValueAccessor {

 

	//@ViewChild('combobox') comboboxElement: MatSelect;
	public Items: ReplaySubject<any[]> = new ReplaySubject<any[]>(1);
	public searchFilterCtrl: FormControl = new FormControl();
	private _onDestroy = new Subject<void>();

    @Input() selectedItem: string = undefined;
	@Output() selectedItemChange: EventEmitter<MatSelectChange> = new EventEmitter<MatSelectChange>();

	@Input() emptyText = '';

	@Input() hint = '';
	@Input() placeholder = '';
	@Input() isRequired: boolean = false;

    @Input() DisplayName = '';
	@Input() allowNullable: boolean = true;


	@Input() groupfield: string;


	private _renderer: Renderer
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
		this._renderer = injector.get(Renderer);

		this.cdr = injector.get(ChangeDetectorRef);
		
    }

    ngAfterViewInit(): void {

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


	protected GetFilter(): any {
		var f = {
			FilterText: this.searchFilterCtrl.value
		}; 
		return f;
	}




	public data: any[];
	public filterItems() {
		if (!this.data) {
			return;
		}
		// get the search keyword
		let search = this.searchFilterCtrl.value;
		if (!search) {
			this.Items.next(this.mapData(this.data.slice()));
			return;
		} else {
			search = search.toLowerCase();
		}
		// filter the banks
		var  f =  this.mapData(this.data.filter(e => e.Description.toLowerCase().indexOf(search) > -1));

		this.Items.next(f);
	}

	mapData(value: Array<any>): Array<any>
	{
		if (this.groupfield) {
			return this.transform(value, this.groupfield);
		}
		return value;
	}





    writeValue(value: any): void {

        var self = this;
        if (value != this.innerValue) {
            this.innerValue = value;
            //setTimeout(() => {
            //     $(self.comboboxElement.nativeElement).selectpicker('refresh');
            //}, 0);
        }
        // this.selectedItem = obj;
        //this.onChange(this.value);
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

		this.Items.next([]);
		this.onSearch();

		this.searchFilterCtrl.valueChanges
		
			.pipe(
				takeUntil(this._onDestroy),
				    debounceTime(200),
					distinctUntilChanged()
			)
			.subscribe(() => {
				this.filterItems();
			});


		//this.filteredOptions = this.myControl.valueChanges
		//	.pipe(
		//		//startWith(null),
		//		debounceTime(200),
		//		distinctUntilChanged(),
		//		switchMap(val => {
		//			return this.filterItems(val || '')
		//		})
		//	);



    }

    onSearch(): void {

	}

	ngOnDestroy() {
		this._onDestroy.next();
		this._onDestroy.complete();
	}




	transform(value: Array<any>, field: string): Array<any> {

		const groupedObj = value.reduce((prev, cur) => {
			if (!prev[cur[field]]) {
				prev[cur[field]] = [cur];
			} else {
				prev[cur[field]].push(cur);
			}
			return prev;
		}, {});
		return Object.keys(groupedObj).map(key => ({ key, value: groupedObj[key] }));
	}

	protected detectChanges(): void {
		try {
			if (this.cdr) {
				this.cdr.detectChanges();
			}
		} catch (e) {

		}
		
	}
}


export abstract class ComboBoxAsync<T extends ADto> extends ComboBoxBaseAsyncComponent implements OnInit, AfterViewInit, ControlValueAccessor {

 
	public Items: ReplaySubject<T[]> = new ReplaySubject<T[]>(1);

    isLoading = false;

    constructor(
        protected service: CrudService<T>,
        protected injector: Injector) {
        super(injector);
	}



    onSearch(): void {
        var self = this;
        this.isLoading = true;
        this.service.requestAllByFilter().subscribe(result => {
			this.Items.next(result.DataObject.Items) ;
			self.isLoading = false;
			
			this.detectChanges();
            //setTimeout(() => {
            //     $(self.comboboxElement.nativeElement).selectpicker('refresh');
            //}, 200);
        });
    }
}







