namespace TextRecognizer.WPF.Utils
{
    public static class RecognizedTextFormatter
    {
        public static string FormatAsIP(string text)
        {
            string result = text.Trim()
                            .Replace(" ", string.Empty)
                            .Replace("\t", string.Empty)
                            .Replace(",", ".")
                            ;

            if (result.IsMatchIP())
                return result;

            LinkedList<char> chars = new LinkedList<char>(result);

            for (int i = 0; i < chars.LongCount(); i++)
            {
                var first = chars.First?.Value;

                if (!first.HasValue)
                {
                    break;
                }

                if (char.IsDigit(first.Value))
                {
                    break;
                }
                else
                {
                    chars.RemoveFirst();
                }
            }

            for (int i = 0; i < chars.LongCount(); i++)
            {
                var last = chars.Last?.Value;

                if (!last.HasValue)
                {
                    break;
                }

                if (char.IsDigit(last.Value))
                {
                    break;
                }
                else
                {
                    chars.RemoveLast();
                }
            }

            result = new string(chars.ToArray());

            return result;
        }
    }
}
