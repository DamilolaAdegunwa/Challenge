import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ResponseModel, PaginListResultDto, ItemDto, ViewMode, FilterDTO, ADto } from '../model/base.model';
import { QueryParamsModel } from '../../models/query-params.model';

export interface Service {
	endpoint: string;
}

@Injectable()
export abstract class CrudService<T extends ADto> implements Service {


	lastFilter$: BehaviorSubject<QueryParamsModel> = new BehaviorSubject(new QueryParamsModel({}, 'asc', '', 0, 10));

	endpoint: string;

	constructor(protected http: HttpClient) {

	}

	requestAllByFilter(reqParams?: any): Observable<ResponseModel<PaginListResultDto<T>>> {
		var params = new HttpParams();
		if (reqParams) {
			Object.keys(reqParams).forEach(function (item) {
				params = params.set(item, reqParams[item]);
			});
		}

		return this.http.get<ResponseModel<PaginListResultDto<T>>>(this.endpoint, { params: params });
	}



	getById(id: any): Observable<ResponseModel<T>> {

		let url = this.endpoint + '/' + id;
		return this.http.get<ResponseModel<T>>(url);
	}



	createOrUpdate(data: T, mode: ViewMode): Observable<ResponseModel<any>> {

		if (mode == ViewMode.Add) {
			return this.http.post<ResponseModel<T>>(this.endpoint, data);
		}
		else {
			let url = this.endpoint + '/' + data.IdValue;
			return this.http.put<ResponseModel<T>>(url, data);
		} 
	}

	delete(id: number): Observable<any> {
		let url = this.endpoint + '/' + id
		return this.http.delete(url);
	}


}
