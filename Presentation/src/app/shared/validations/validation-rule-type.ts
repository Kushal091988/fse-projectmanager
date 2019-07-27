export enum ValidationRuleType {
  required = 1,
  min = 2,
  max = 3,
  pattern = 4, // (for a regex match)
  async = 5 // for custom async validation
}
