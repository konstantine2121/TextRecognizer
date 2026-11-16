using System.Text.RegularExpressions;
using System.Windows;
using TextRecognizer.WPF.Utils;
using System.Windows.Media.Imaging;
using TextRecognizer.Core.Recognizers;

namespace TextRecognizer.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BitmapFrame? _currentFrame;
        private readonly IRecognizer _englishRecognizer;
        private readonly IRecognizer _russianRecognizer;

        public MainWindow()
        {
            InitializeComponent();
            _englishRecognizer = new SimpleEnglishRecognizer();
            _russianRecognizer = new SimpleRussianRecognizer();
        }

        private IRecognizer Recognizer => 
            RussianRadioButton.IsChecked != null && RussianRadioButton.IsChecked.Value 
                ? _russianRecognizer 
                : _englishRecognizer;
        
        private void CopyImageFromClipbardButton_Click(object sender, RoutedEventArgs e)
        {
            if (Utils.ImageFromClipboardCopyHelper.TryGetImageFromClipboardDib(out var imageSource))
            {
                _currentFrame = imageSource;
            }
            else
            {
                _currentFrame = null;
            }
            
            ClipboardDataImage.Source = _currentFrame;
        }

        private void RecognizeTextButton_Click(object sender, RoutedEventArgs e)
        {
            if (_currentFrame is null)
            {
                RecognizedTextOutputTextBox.Text = "No image to recognize!";
            }
            else
            {
                RecognizedTextOutputTextBox.Text = Recognizer.Recognize(_currentFrame, out _);
            }
        }

        private void RecognizeIpButton_Click(object sender, RoutedEventArgs e)
        {
            RecognizeTextButton_Click(sender, e);
            var text = RecognizedTextOutputTextBox.Text;
            RecognizedTextOutputTextBox.Text = FormatAsIP(text);             
        }

        private static string FormatAsIP(string text)
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

        

        private void PerformFullRecognizeActionsButton_Click(object sender, RoutedEventArgs e)
        {
            CopyImageFromClipbardButton_Click(sender, e);
            RecognizeTextButton_Click(sender, e);
        }

        
    }
}