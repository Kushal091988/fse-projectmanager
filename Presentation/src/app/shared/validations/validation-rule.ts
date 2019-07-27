import { Observable } from 'rxjs';

import { ValidationRuleType } from './validation-rule-type';

export interface ValidationRule {

  /** unique id */
  id: string;

  /** types of validation */
  type: ValidationRuleType;

  /** object field name */
  field: string;

  /** validation rule value
   * min: number
   * max: number
   * pattern: string
   **/
  ruleValue?: any;

  /** message to show when validation failed */
  message?: string;

  /** predicate to execute for [ValidationRuleType.Custom] */
  predicate?: (value: string) => void;
}
