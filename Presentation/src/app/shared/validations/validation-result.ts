export interface ValidationResult {
  /** unique id */
  id: string;
  /** object field name */
  field: string;
  /** is validation failed */
  invalid: boolean;
  /** message to show when validation failed */
  message: string;
  /** indicate if result pending for callback. only applicable to async method */
  pending: boolean;
}
