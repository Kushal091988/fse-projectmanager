import { arrayHelper } from 'src/app/core/array.helper';
import { common } from 'src/app/core/common';

import { ValidationResult } from './validation-result';
import { ValidationResults } from './validation-results';
import { ValidationRule } from './validation-rule';
import { ValidationRuleType } from './validation-rule-type';

export abstract class BaseValidator<T> {

  private rules: ValidationRule[] = [];
  private rulesDict: { [id: string]: ValidationRule } = {};

  /** results dictionary, useful for async validation */
  private resultsDict: { [id: string]: ValidationResult } = {};

  public validationResultsValue: ValidationResults;

  constructor(rules: ValidationRule[]) {
    this.rules = rules;

    const results = [];
    this.rules.forEach(r => {
      this.rulesDict[r.id] = r;

      const result: ValidationResult = {
        id: r.id,
        field: r.field,
        message: r.message,
        pending: false,
        invalid: false // default to valid
      };

      results.push(result);

      if (!common.isNil(this.resultsDict[r.id])) {
        throw new Error(`ValidationRule.Id is not unique!: ${r.id}}`);
      } else {
        // construct dictionary
        this.resultsDict[r.id] = result;
      }
    });

    this.validationResultsValue = new ValidationResults(results);
  }

  /** validate min value. return false if invalid */
  private min(v: any, min: number): boolean {

    if (common.isNil(v)) { return true; }

    if (common.isNumber(v) && v < min) {
      return true;
    }

    if (v.toString().length === 0 || v.toString().length < min) {
      return true;
    }

    return false;
  }

  /** validate mandatory field. return false if invalid */
  private required(v: any) {

    if (common.isNil(v)) { return true; }

    if (v.toString().length === 0 || v.toString().length === 0) {
      return true;
    }

    if (common.isNumber(v) && v === 0) {
      return true;
    }

    return false;
  }

  protected rule(id: string): ValidationRule {
    return this.rulesDict[id];
  }

  /** to set pending flag to true */
  public validationAsyncStart(id: string) {
    this.resultsDict[id].pending = true;
  }

  /** to set pending flag to false */
  public validationAsyncEnd(id: string, invalid: boolean) {
    this.resultsDict[id].pending = false;
    this.resultsDict[id].invalid = invalid;
  }

  /** indicate if result pending async validation */
  public pending(id: string): boolean {
    return this.resultsDict[id].pending;
  }

  public get validationResults(): ValidationResults {
    return this.validationResultsValue;
  }

  public asyncResult(id: string): ValidationResult {
    return this.resultsDict[id];
  }

  public validate(obj: T) {
    const results = [];

    this.rules.forEach(r => {
      const result = this.resultsDict[r.id];

      switch (r.type) {
        case ValidationRuleType.required:
          result.invalid = this.required(obj[r.field]);
          break;

        case ValidationRuleType.async:
          if (result.pending) {

            /** By right, expected no invocation of validation() func if any of the results.pending === true;
             * due to results is still async validation, therefore, set {invalid = true} while waiting.
             * WARNING! this might cause unexpected behavior, for example: let's say previously invalid === false,
             * user re-key in something that trigger async validations again, this time, the invalid will be set to true
             * even it's still pending async callback()
             */
            result.invalid = true;
          }
          break;

        default:
          console.error(`rules not handled`);
      }

      results.push(result);
    });

    this.validationResultsValue = new ValidationResults(results);
  }
}
