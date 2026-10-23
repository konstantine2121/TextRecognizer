using System.Diagnostics;
using Tesseract;

namespace TextRecognizer.Core.Recognizers
{
    public abstract class BaseRecognizer : IRecognizer
    {
        private const string ModelsDirectoryName = "tessdata";

        protected string BaseDirectory => AppDomain.CurrentDomain.BaseDirectory;

        protected string ModelsDirectoryDatapath => Path.Combine(BaseDirectory, ModelsDirectoryName);

        protected TesseractEngine Engine { get; }

        protected BaseRecognizer(string language, EngineMode engineMode)
        {
            Engine = CreateEngine(language, engineMode) ?? throw new NullReferenceException(nameof(Engine));
        }

        protected virtual TesseractEngine CreateEngine(string language, EngineMode engineMode)
        {
            return new TesseractEngine(ModelsDirectoryDatapath, language, engineMode);
        }

        public virtual string Recognize(MemoryStream memoryStream, out float meanConfidence)
        {
            meanConfidence = 0.0f;

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
                var errMessage = FormatExceptionMessage(e);
                return errMessage;
            }
        }

        protected virtual string FormatExceptionMessage(Exception exception)
        {
            string newLine = Environment.NewLine;

            return "Unexpected Error: " + exception.Message + newLine
                + "Details: " +  newLine
                + exception.ToString();
        }

        public void Dispose()
        {
            Engine?.Dispose();
        }
    }
}
