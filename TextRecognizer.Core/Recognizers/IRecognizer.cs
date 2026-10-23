using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRecognizer.Core.Recognizers
{
    public interface IRecognizer
    {
        string Recognize(MemoryStream memoryStream, out float meanConfidence);
    }
}
