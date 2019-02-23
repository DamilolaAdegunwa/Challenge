import { Injectable } from '@angular/core';
import swal from 'sweetalert2';
//import * as $ from 'jquery'

//export declare function swal(message: string, title?: string);
//export declare function swal(title: string, message?: string, type?: string): Promise<any>;
//export declare function swal(message: string, title?: string);
//export declare function swal(settings: any): Promise<any>;

@Injectable()
export class MessageService {



    constructor() {
        swal.setDefaults({
            width: 400,
            padding: 25,
            buttonsStyling: false,
            confirmButtonClass: 'btn btn-success m-btn m-btn--custom',
            confirmButtonColor: null,
            cancelButtonClass: 'btn btn-secondary m-btn m-btn--custom',
            cancelButtonColor: null
        });
    }

    info(message: string, title?: string): any {
        return swal(title, message, 'info')
    }

    success(message: string, title?: string): any {
        return swal(title, message, 'success');
    }

    warn(message: string, title?: string): any {
        return swal(title, message, 'warning');
    }

    error(message: string, title?: string): any {
        return swal(title, message, 'error');
    }


    confirm(message: string, title?: string, callback?: (result: any) => void): any {

        return swal(
            {
                title: title,
                text: message,
                type: 'question',
                showCancelButton: true,
                confirmButtonText: 'Aceptar',
                cancelButtonText: 'Cancelar',
            }

        ).then(function(isConfirmed) {
            callback && callback(isConfirmed);
        });

    }

}
