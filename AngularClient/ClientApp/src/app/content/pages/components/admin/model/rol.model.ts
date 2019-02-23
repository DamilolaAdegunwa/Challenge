
import * as moment from 'moment';
import { Dto, FilterDTO } from '../../../../../core/shared/model/base.model';


export class RolDto extends Dto<number> {
    getDescription(): string {
        return this.DisplayName;
    }
    Name: string
    DisplayName: string;
    IsDefault: boolean;
	IsStatic: boolean;
	AssignedUsers: number;

	GrantedPermissionNames: string[] = [];
}


export class RolFilter extends FilterDTO {
    Id: number;
    Page: number;
    PageSize: number;
    Sort: String;
}
