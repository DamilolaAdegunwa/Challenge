import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";
import { Observable } from "rxjs";
import { UserService } from "../../../../core/services/user.service";
import { NgxPermissionsGuard, NgxPermissionsService, NgxRolesService } from "ngx-permissions";

@Injectable()
export class AnonymousGuard extends NgxPermissionsGuard implements CanActivate {

	constructor(private _router: Router, private _userService: UserService, private _permissionsService: NgxPermissionsService, _rolesService: NgxRolesService) {
		super(_permissionsService, _rolesService, _router);
	}

	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Promise<boolean> | boolean {
		let token = localStorage.accessToken;
		let refreshToken = localStorage.refreshToken;

		if (token == null || new Date(refreshToken) < new Date()) {
			super.canActivate(route, state);
            return true;
        }
		
        this._router.navigate(['/'], { queryParams: { returnUrl: state.url } });
        return false;
    }
}
