using System.Windows;
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

        public MainWindow()
        {
            InitializeComponent();
        }

        private SimpleEnglishRecognizer Recognizer { get; } = new SimpleEnglishRecognizer();
        
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

        private void PerformFullRecognizeActionsButton_Click(object sender, RoutedEventArgs e)
        {
            CopyImageFromClipbardButton_Click(sender, e);
            RecognizeTextButton_Click(sender, e);
        }
    }
}