namespace ProjectManager.SharedKernel.FilterCriteria
{
    public class RangeFilter<T>
    {
        public T From { get; set; }

        public T To { get; set; }

        /// <summary>
        /// Time zone offset (minutes) with client browser.
        /// Only applicable to DateTime
        /// </summary>
        public double? TimeZoneOffset { get; set; }
    }
}