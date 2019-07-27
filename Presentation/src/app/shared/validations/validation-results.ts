import * as _ from 'lodash';
import { arrayHelper } from 'src/app/core/array.helper';
import { common } from 'src/app/core/common';

import { ValidationResult } from './validation-result';

export class ValidationResults {
  public results: ValidationResult[] = [];
  private resultDict: { [key: string]: ValidationResult } = {};

  constructor(results: ValidationResult[]) {
    this.results = results;

    this.results.forEach(r => {
      if (!common.isNil(this.resultDict[r.id])) {
        throw new Error(`${r.id}: validation rules is not unique`);
      }
      this.resultDict[r.id] = r;
    });
  }

  get isValid(): boolean {
    return common.isNil(arrayHelper.firstOrDefault(this.results, (x: ValidationResult) => !x.invalid));
  }

  public result(id: string): ValidationResult {
    return this.resultDict[id];
  }
}
