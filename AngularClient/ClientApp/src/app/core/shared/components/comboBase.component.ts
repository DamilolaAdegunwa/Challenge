import { Component, OnInit, AfterViewInit, AfterViewChecked, ElementRef, ViewChild, Injector, Input, Output, EventEmitter, Renderer, forwardRef, SimpleChanges, SimpleChange, ChangeDetectorRef } from '@angular/core';
import { NgForm, FormGroup, ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { ADto } from '../model/base.model';
import { CrudService } from '../services/crud.service';
import { MatSelect, MatSelectChange } from '@angular/material';

export abstract class ComboBoxBaseComponent implements OnInit, AfterViewInit, ControlValueAccessor {

	@ViewChild('combobox') comboboxElement: MatSelect;

    @Input() selectedItem: string = undefined;
	@Output() selectedItemChange: EventEmitter<MatSelectChange> = new EventEmitter<MatSelectChange>();

	@Input() emptyText = '';

	@Input() hint = '';
	@Input() placeholder = '';
	@Input() isRequired: boolean = false;
	@Input() IsDisabled: boolean = false;

    @Input() DisplayName = '';
	@Input() allowNullable: boolean = true;

	@Input() floatLabel = 'auto';

	private _renderer: Renderer
	protected cdr: ChangeDetectorRef;
	
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
        this.onSearch();
    }

    onSearch(): void {

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


export abstract class ComboBoxComponent<T extends ADto> extends ComboBoxBaseComponent implements OnInit, AfterViewInit, ControlValueAccessor {

    items: T[] = [];



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
            this.items = result.DataObject.Items;
			self.isLoading = false;
			this.detectChanges();
            //setTimeout(() => {
            //     $(self.comboboxElement.nativeElement).selectpicker('refresh');
            //}, 200);
        });
    }
}







