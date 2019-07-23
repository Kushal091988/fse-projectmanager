import { ConfirmationService } from 'primeng/api';
import { Injectable } from '@angular/core';
import { common } from '../common';

@Injectable({
    providedIn: 'root'
})
export class ConfirmationDialogService {

    constructor(private confirmationService: ConfirmationService) { }

    confirm(confirmationMsg: string, acceptCallback: Function, rejectCallback?: Function) {
        this.confirmationService.confirm({
            message: common.isArray(confirmationMsg) ||
                confirmationMsg.length === 0 ? 'Are you sure that you want to proceed?' : confirmationMsg,
            header: 'Confirmation',
            icon: 'pi pi-exclamation-triangle',
            accept: () => {
                acceptCallback();
            },
            reject: () => {
                if (!common.isNil(rejectCallback)) {
                    rejectCallback();
                }
            }
        });
    }
}
