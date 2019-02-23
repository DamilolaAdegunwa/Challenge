import { Injectable } from '@angular/core';
import { NgxPermissionsService } from 'ngx-permissions';
//import { Observer } from 'rxjs/Rx';

import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
//import { BehaviorSubject } from 'rxjs/BehaviorSubject';
//import { Observable } from 'rxjs/Observable';
import { LocalStorageService } from './services/storage.service';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../auth/authentication.service';



@Injectable()
export class PermissionCheckerService {

    endpoint: string;
    private _PermissionList: string[];

    constructor(private permissionsService: NgxPermissionsService,
		private authService: AuthenticationService,
        protected http: HttpClient) {
        this._PermissionList = [];

		this.endpoint = environment.apiUrl;
		

    }

	isGranted(permissionName: string): boolean {
        return this._PermissionList.indexOf(permissionName) != -1;
    }

	hasPermission(permissionName: string): Promise<boolean> {
		return this.permissionsService.hasPermission(permissionName);		 
	}

	loadPermissions(permisos: any): void {     
		this._PermissionList = permisos;        
    }




    //setPermissions(per): void {
    //    //buscar en servicio         
    //    return this._localStorageService.store('permisos_usuario', per);
    //}



    GetPermissions(): Observable<any> {
        //buscar en servicio 
        return this.http.get<any>(this.endpoint + '/api/Permission');
    }


    //clearPermissions(): void {
    //    this._localStorageService.removeItem('permisos_usuario');
    //    this.permissionsService.flushPermissions();
    //}
}


