using System.Text.RegularExpressions;

namespace TextRecognizer.WPF.Utils
{
    public static class StringExtensions
    {
        public static bool IsMatchIP(this string value)
        {
            const string standardRegexForIP = "^(\\d{1,3}\\.){3}\\d{1,3}$";

            bool regexIsMatch = Regex.IsMatch(value, standardRegexForIP);

            if (!regexIsMatch)
            {
                return false;
            }

            var values = value.Split(".", StringSplitOptions.RemoveEmptyEntries);
            foreach (var v in values)
            {
                if (!int.TryParse(v, out var val) || (val < 0 || val > 255))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
