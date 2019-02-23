import { Injectable, ErrorHandler } from "@angular/core"; 
import { GlobalErrorLogService } from './global-errorlog.Service';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

    constructor(private errorLogService: GlobalErrorLogService) {
    }

    handleError(error: any): void {
        this.errorLogService.logError(error);
    }
}


