using Tesseract;

namespace TextRecognizer.Core.Recognizers
{
    public class SimpleEnglishRecognizer : BaseRecognizer
    {
        public SimpleEnglishRecognizer() 
            : base(SupportedLanguages.English, EngineMode.Default)
        {
        }
    }
}
