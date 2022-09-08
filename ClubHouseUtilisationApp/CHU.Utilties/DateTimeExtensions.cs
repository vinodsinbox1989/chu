using System.Globalization;

namespace CHUUtilties
{
    public static class DateTimeExtensions
    {
        public static bool IsValidDateTime(this string dateTime)
        {
            if (string.IsNullOrEmpty(dateTime))
            {
                return false;
            }
            string[] formats = { "dd/MM/yyyy hh:mm" };
            return DateTime.TryParseExact(dateTime, formats, new CultureInfo("en-US"),
                                           DateTimeStyles.None, out DateTime parsedDateTime);
        }
    }
}
