using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tesseract;

namespace TextRecognizer.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ImageSource _currentImage;

        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void CopyImageFromClipbardButton_Click(object sender, RoutedEventArgs e)
        {
            if (TextRecognizer.WPF.Utils.ImageFromClipboardCopyHelper.TryGetImageFromClipboardDib(out var imageSource))
            {
                _currentImage = imageSource;
            }
            else
            {
                _currentImage = null;
            }
            
            ClipboardDataImage.Source = _currentImage;
        }

       
    }
}