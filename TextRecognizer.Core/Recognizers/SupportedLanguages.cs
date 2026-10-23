namespace TextRecognizer.Core.Recognizers
{
    public static class SupportedLanguages
    {
        public const string English = "eng";
        public const string Russian = "rus";

        public static IReadOnlyList<string> Languages => new string[]
            {
                English,
                Russian,
            };
    }
}
