using System.Globalization;

namespace TheSharks.Domain.Extensions;

public static class DateTimeExtensions
{
    private static DateTimeFormatInfo GetCultureDateInfo(string language)
    {
        return CultureInfo.GetCultureInfo(language).DateTimeFormat;
    }

    public static string LongDateFormat(DateTimeOffset date, string language)
    {
        string format = GetCultureDateInfo(language).LongDatePattern;
        return date.ToString(format);
    }
}