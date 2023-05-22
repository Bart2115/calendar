using System.Text.RegularExpressions;

namespace Calendar.Notes.Helpers
{
    public static class DateValidator
    {
        public static bool IsValidDate(string date)
        {
            var pattern = "[0-1][0-9]\\/[0-3][0-9]\\/[1-2][0-9][0-9][0-9]";
            Regex rg = new Regex(pattern);
            var regexValidated = rg.IsMatch(date);
            if (regexValidated)
            {
                try
                {
                    DateTime.ParseExact(date, "mm/dd/yyyy", null);
                    return true;
                }
                catch{ }
            }
            return false;
        }
    }
}
