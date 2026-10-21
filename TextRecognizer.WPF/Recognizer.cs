using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;
using Tesseract;
using TextRecognizer.WPF.Utils;

namespace TextRecognizer.WPF
{
    class Recognizer : IDisposable
    {
        public TesseractEngine Engine { get; private set; }

        public Recognizer()
        {
            Init();
        }

        private void Init()
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var tesseractDirectoryDatapath = Path.Combine(baseDir, "tessdata");
            Engine = new TesseractEngine(tesseractDirectoryDatapath, "eng", EngineMode.Default);
        }

        public string Recognize(BitmapFrame frame, out double meanConfidence)
        {
            return Recognize(frame.ConvertToMemoryStream(), out meanConfidence);
        }

        public string Recognize(MemoryStream memoryStream, out double meanConfidence)
        {
            meanConfidence = 0.0;

            try
            {
                using (var img = Pix.LoadFromMemory(memoryStream.ToArray()))
                {
                    using (var page = Engine.Process(img))
                    {
                        var text = page.GetText();

                        meanConfidence = page.GetMeanConfidence();
                        return text;
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                var errMessage = "Unexpected Error: " + e.Message + Environment.NewLine + "Details: " + Environment.NewLine + e.ToString();
                return errMessage;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
