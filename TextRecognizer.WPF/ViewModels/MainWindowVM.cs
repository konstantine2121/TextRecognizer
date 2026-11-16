using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Media.Imaging;
using TextRecognizer.Core.Recognizers;
using TextRecognizer.WPF.Utils;

namespace TextRecognizer.WPF.ViewModels
{
    public class MainWindowVM : ObservableObject
    {
        private readonly IRecognizer _englishRecognizer;
        private readonly IRecognizer _russianRecognizer;

        private BitmapFrame? _currentFrame = null;
        private bool? _englishIsSelected = true;
        private bool? _russianIsSelected = false;
        private string? _recognizedText;
        private float _meanConfidence = 0.0f;
        
        public MainWindowVM()
        {
            _englishRecognizer = new SimpleEnglishRecognizer();
            _russianRecognizer = new SimpleRussianRecognizer();

            CopyImageFromClipbardCommand = new RelayCommand(CopyImageFromClipbard);
            RecognizeTextCommand = new RelayCommand(RecognizeText);
            RecognizeIpCommand = new RelayCommand(RecognizeIp);
            PerformCopyImageAndRecognizeTextCommand = new RelayCommand(PerformCopyImageAndRecognizeText);
            PerformCopyImageAndRecognizeIpCommand = new RelayCommand(PerformCopyImageAndRecognizeIp);
        }

        private IRecognizer Recognizer => EnglishIsSelected != null && EnglishIsSelected.Value
        ? _englishRecognizer
        : _russianRecognizer;

        public bool? EnglishIsSelected
        {
            get => _englishIsSelected;
            set
            {
                if (SetProperty(ref _englishIsSelected, value))
                {
                    if (value.HasValue)
                    {
                        RussianIsSelected = !value.Value;
                    }
                }
            }
        }

        public bool? RussianIsSelected
        {
            get => _russianIsSelected;
            set
            {
                if (SetProperty(ref _russianIsSelected, value))
                {
                    if (value.HasValue)
                    {
                        EnglishIsSelected = ! value.Value;
                    }
                }
            }
        }

        public BitmapFrame? CurrentFrame
        {
            get => _currentFrame;
            private set => SetProperty(ref _currentFrame, value);            
        }

        public string? RecognizedText 
        { 
            get => _recognizedText; 
            private set => SetProperty(ref _recognizedText, value); 
        }

        public float MeanConfidence
        {
            get => _meanConfidence;
            private set => SetProperty(ref _meanConfidence, value);
        }

        public IRelayCommand CopyImageFromClipbardCommand { get; }
        public IRelayCommand RecognizeTextCommand { get; }
        public IRelayCommand RecognizeIpCommand { get; }
        public IRelayCommand PerformCopyImageAndRecognizeTextCommand { get; }
        public IRelayCommand PerformCopyImageAndRecognizeIpCommand { get; }


        private void CopyImageFromClipbard()
        {
            if (Utils.ImageFromClipboardCopyHelper.TryGetImageFromClipboardDib(out var imageSource))
            {
                CurrentFrame = imageSource;
            }
            else
            {
                CurrentFrame = null;
            }
        }

        private void RecognizeText()
        {
            if (_currentFrame is null)
            {
                RecognizedText = "No image to recognize!";
            }
            else
            {
                RecognizedText = Recognizer.Recognize(_currentFrame, out float meanConfidence);
                MeanConfidence = meanConfidence;
            }
        }

        private void RecognizeIp()
        {
            var recognizedText = Recognizer.Recognize(_currentFrame, out float meanConfidence);
            RecognizedText = RecognizedTextFormatter.FormatAsIP(recognizedText);
            MeanConfidence = meanConfidence;
        }

        private void PerformCopyImageAndRecognizeText()
        {
            CopyImageFromClipbard();
            RecognizeText();
        }

        private void PerformCopyImageAndRecognizeIp()
        {
            CopyImageFromClipbard();
            RecognizeIp();
        }
    }
}
