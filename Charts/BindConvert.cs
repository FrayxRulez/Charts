using System.Linq;
using Windows.Globalization.DateTimeFormatting;
using Windows.System.UserProfile;

namespace Unigram.Converters
{
    public class BindConvert
    {
        private static BindConvert _current;
        public static BindConvert Current
        {
            get
            {
                if (_current == null)
                    _current = new BindConvert();

                return _current;
            }
        }

        public DateTimeFormatter ShortDate { get; private set; }
        public DateTimeFormatter ShortTime { get; private set; }
        public DateTimeFormatter LongDate { get; private set; }
        public DateTimeFormatter LongTime { get; private set; }

        public DateTimeFormatter MonthFull { get; private set; }
        public DateTimeFormatter MonthAbbreviatedDay { get; private set; }
        public DateTimeFormatter MonthFullYear { get; private set; }
        public DateTimeFormatter DayMonthFull { get; private set; }
        public DateTimeFormatter DayMonthFullYear { get; private set; }



        private BindConvert()
        {
            //var region = new GeographicRegion();
            //var code = region.CodeTwoLetter;

            //var culture = NativeUtils.GetCurrentCulture();
            //var languages = new[] { culture }.Union(GlobalizationPreferences.Languages);
            var languages = GlobalizationPreferences.Languages;

            ShortDate = new DateTimeFormatter("shortdate", languages, GlobalizationPreferences.HomeGeographicRegion, GlobalizationPreferences.Calendars.FirstOrDefault(), GlobalizationPreferences.Clocks.FirstOrDefault());
            ShortTime = new DateTimeFormatter("shorttime", languages, GlobalizationPreferences.HomeGeographicRegion, GlobalizationPreferences.Calendars.FirstOrDefault(), GlobalizationPreferences.Clocks.FirstOrDefault());
            LongDate = new DateTimeFormatter("longdate", languages, GlobalizationPreferences.HomeGeographicRegion, GlobalizationPreferences.Calendars.FirstOrDefault(), GlobalizationPreferences.Clocks.FirstOrDefault());
            LongTime = new DateTimeFormatter("longtime", languages, GlobalizationPreferences.HomeGeographicRegion, GlobalizationPreferences.Calendars.FirstOrDefault(), GlobalizationPreferences.Clocks.FirstOrDefault());
            MonthFull = new DateTimeFormatter("month.full", languages, GlobalizationPreferences.HomeGeographicRegion, GlobalizationPreferences.Calendars.FirstOrDefault(), GlobalizationPreferences.Clocks.FirstOrDefault());
            MonthAbbreviatedDay = new DateTimeFormatter("month.abbreviated day", languages, GlobalizationPreferences.HomeGeographicRegion, GlobalizationPreferences.Calendars.FirstOrDefault(), GlobalizationPreferences.Clocks.FirstOrDefault());
            MonthFullYear = new DateTimeFormatter("month.full year", languages, GlobalizationPreferences.HomeGeographicRegion, GlobalizationPreferences.Calendars.FirstOrDefault(), GlobalizationPreferences.Clocks.FirstOrDefault());
            DayMonthFull = new DateTimeFormatter("day month.full", languages, GlobalizationPreferences.HomeGeographicRegion, GlobalizationPreferences.Calendars.FirstOrDefault(), GlobalizationPreferences.Clocks.FirstOrDefault());
            DayMonthFullYear = new DateTimeFormatter("day month.full year", languages, GlobalizationPreferences.HomeGeographicRegion, GlobalizationPreferences.Calendars.FirstOrDefault(), GlobalizationPreferences.Clocks.FirstOrDefault());
        }
    }
}
