export interface Credential {
	username: string;
	email: string;
	password: string;
}


export interface IResetPasswordInput {
	Password: string;
	ConfirmPassword: string;
	OldPassword: string; 
	Username: string;

}


export class ResetPasswordInput implements IResetPasswordInput {
	Password: string;
	ConfirmPassword: string;
	OldPassword: string;
	Username: string;

}
