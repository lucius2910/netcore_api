namespace Framework.Core.Extensions
{
    public static class DateTimeExtensions
    {
        private static string _dateFormat = "yyyy/MM/dd";
        private static string _dateFileFormat = "yyyyMMdd";
        private static string _dateTimeFormat = "yyyy/MM/dd hh:mm:ss";
        private static string _dateFileTimeFormat = "yyyyMMddhhmmss";

        public static DateTime? ToDate(this string dateStr)
        {
            var res = DateTime.Now;
            var trypa = DateTime.TryParse(dateStr, out res);
            return trypa ? res : null;
        }
        public static DateTime? ToDateTime(this string datetimeStr)
        {
            var resTime = DateTime.Now;
            var trypa = DateTime.TryParse(datetimeStr, out resTime);
            return trypa ? resTime : null;
        }

        public static string ToDateString(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString(_dateFormat) : string.Empty;
        }

        public static string ToDateTimeString(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString(_dateTimeFormat) : string.Empty;
        }

        public static string ToDateString(this DateTimeOffset? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString(_dateFormat) : string.Empty;
        }
        
        public static string ToDateTimeString(this DateTimeOffset? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString(_dateTimeFormat) : string.Empty;
        }
        
        public static string ToDateString(this DateTimeOffset dateTime)
        {
            return dateTime.ToString(_dateFormat);
        }

        public static string ToDateTimeStringFile(this DateTimeOffset? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString(_dateFileTimeFormat) : string.Empty;
        }

        public static string ToDateStringFile(this DateTime dateTime)
        {
            return dateTime.ToString(_dateFileFormat);
        }

        public static string ToDateTimeString(this DateTime dateTime)
        {
            return dateTime.ToString(_dateTimeFormat);
        }

        public static string ToDateString(this DateTime dateTime)
        {
            return dateTime.ToString(_dateFormat);
        }

        public static string GetMonth(this DateTime dateTime)
        {
            if (dateTime.Month == DateTime.Now.Month)
            {
                return "今月";
            }
            return dateTime.Month + "月";

        }

        public static List<DateTime> GetDates(int year, int month)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))  // Days: 1, 2 ... 31 etc.
                             .Select(day => new DateTime(year, month, day)) // Map each day to a date
                             .ToList(); // Load dates into a list
        }
    }
}
