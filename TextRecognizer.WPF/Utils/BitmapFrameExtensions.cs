using System.IO;
using System.Windows.Media.Imaging;

namespace TextRecognizer.WPF.Utils
{
    public static class  BitmapFrameExtensions
    {
        public static MemoryStream ConvertToMemoryStream(this BitmapFrame frame)
        {
            MemoryStream memoryStream = new MemoryStream();            
            BitmapEncoder encoder = new BmpBitmapEncoder(); // Choose Png, Jpeg, Bmp, etc.            
            encoder.Frames.Add(frame);
            encoder.Save(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream;
        }
    }
}
