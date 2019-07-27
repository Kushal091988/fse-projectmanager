import { BaseValidator } from 'src/app/shared/validations/base-validator';

import { Injectable } from '@angular/core';

import { appSettings } from '../../app.settings';
import { AppHttpService } from '../../core/api/app-http.service';
import { EntityType } from '../enums';

@Injectable({
  providedIn: 'root'
})
export class ValidationService {

  constructor(private httpService: AppHttpService) { }

  /** common share code unique validation func() */
  public codeUniqueValidationAsync<T>(entityType: EntityType, id: string, value: string, validator: BaseValidator<T>) {
    validator.validationAsyncStart(id);

    // setTimeout(() => {
    //   const isUnique = false;
    //   validator.validationAsyncEnd(id, !isUnique);
    // }, 2000);
  }
}
