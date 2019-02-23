import {
	Component,
	OnInit,
	Input,
	Output,
	ViewChild,
	ElementRef,
	ChangeDetectorRef
} from '@angular/core';
import { Subject } from 'rxjs';
import { AuthenticationService } from '../../../../core/auth/authentication.service';
import { NgForm } from '@angular/forms';
import * as objectPath from 'object-path';
import { AuthNoticeService } from '../../../../core/auth/auth-notice.service';
import { SpinnerButtonOptions } from '../../../partials/content/general/spinner-button/button-options.interface';
import { TranslateService } from '@ngx-translate/core';
import { finalize, catchError } from 'rxjs/operators';
import { HttpErrorResponse } from '@angular/common/http';
import { debug } from 'util';

@Component({
	selector: 'm-reset-password',
	templateUrl: './reset-password.component.html',
	styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
	public model: any = { email: '' };
	@Input() action: string;
	@Output() actionChange = new Subject<string>();
	public loading = false;

	@ViewChild('f') f: NgForm;
	errors: any = [];

	spinner: SpinnerButtonOptions = {
		active: false,
		spinnerSize: 18,
		raised: true,
		buttonColor: 'primary',
		spinnerColor: 'accent',
		fullWidth: false
	};

	constructor(
		private authService: AuthenticationService,
		public authNoticeService: AuthNoticeService,
		private translate: TranslateService,
		private cdr: ChangeDetectorRef
	) { }

	ngOnInit() {
		this.authNoticeService.setNotice('Debe actualizar su contraseña.', 'info');
	}

	loginPage(event: Event) {
		event.preventDefault();
		this.action = 'login';
		this.actionChange.next(this.action);

		this.authNoticeService.setNotice('');
	}

	submit() {
		this.spinner.active = true;
		if (this.validate(this.f, this.model)) {
			this.authService.requestPassword(this.model)
			
				.pipe(					
					catchError((err: HttpErrorResponse) => {
					
						if (err) {
							if (err.error) {
								this.authNoticeService.setNotice(err.error, 'error');
							}
						}
						this.spinner.active = false;
						this.cdr.detectChanges();
						return undefined;
					}),
					finalize(() => {
					
						this.spinner.active = false;
						this.authNoticeService.setNotice(null);
						this.authNoticeService.setNotice("Se ha reseteado la contraseña del usuario: " + this.model.Username + " exitosamente", 'success');
						this.cdr.detectChanges();

					})

				)
				.subscribe(response => {
					
					if (typeof response !== 'undefined') {
						this.action = 'login';
						this.actionChange.next(this.action);
					} else {
						// tslint:disable-next-line:max-line-length
						this.authNoticeService.setNotice(this.translate.instant('AUTH.VALIDATION.NOT_FOUND', { name: this.translate.instant('AUTH.INPUT.EMAIL') }), 'error');
					}
					this.spinner.active = false;
					this.cdr.detectChanges();
				});
		}
	}

	validate(f: NgForm, model: any) {
		
		if (objectPath.get(f, 'form.controls.Password.value') != objectPath.get(f, 'form.controls.ConfirmPassword.value')) {
			this.errors.push('Las contraseñas no coinciden.');
			this.authNoticeService.setNotice(this.errors.join('<br/>'), 'error');
			this.spinner.active = false;
			return false;
		}

		if (f.form.status === 'VALID') {
			return true;
		}

		if (objectPath.get(f, 'form.controls.Username.errors.required')) {
			this.errors.push(this.translate.instant('AUTH.VALIDATION.INVALID', { name: 'Usuario' }));
		}

		if (objectPath.get(f, 'form.controls.OldPassword.errors.required')) {
			this.errors.push(this.translate.instant('AUTH.VALIDATION.INVALID', { name: 'Contraseña Actual' }));
		}
		if (objectPath.get(f, 'form.controls.OldPassword.errors.minlength')) {
			this.errors.push(this.translate.instant('AUTH.VALIDATION.MIN_LENGTH', { name: 'Contraseña Actual' }));
		}


		if (objectPath.get(f, 'form.controls.Password.errors.required')) {
			this.errors.push(this.translate.instant('AUTH.VALIDATION.INVALID', { name: 'Nueva Contraseña' }));
		}
		if (objectPath.get(f, 'form.controls.Password.errors.minlength')) {
			this.errors.push(this.translate.instant('AUTH.VALIDATION.MIN_LENGTH', { name: 'Nueva Contraseña' }));
		}

		if (this.errors.length > 0) {
			this.authNoticeService.setNotice(this.errors.join('<br/>'), 'error');
			this.spinner.active = false;
		}

		return false;
	}
}
