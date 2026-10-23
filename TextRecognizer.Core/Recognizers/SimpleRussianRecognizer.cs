using Tesseract;

namespace TextRecognizer.Core.Recognizers
{
    public class SimpleRussianRecognizer : BaseRecognizer
    {
        public SimpleRussianRecognizer()
            : base(SupportedLanguages.Russian, EngineMode.Default)
        {
        }
    }
}
