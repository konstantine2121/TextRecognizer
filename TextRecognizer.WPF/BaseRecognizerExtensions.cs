using System.Windows.Media.Imaging;
using TextRecognizer.Core.Recognizers;
using TextRecognizer.WPF.Utils;

namespace TextRecognizer.WPF
{
    public static class BaseRecognizerExtensions
    {
        public static string Recognize(this BaseRecognizer recognizer, BitmapFrame frame, out float meanConfidence)
        {
            return recognizer.Recognize(frame.ConvertToMemoryStream(), out meanConfidence);
        }
    }
}
