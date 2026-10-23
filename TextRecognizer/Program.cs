using System.Diagnostics;
using Tesseract;

namespace TextRecognizer.CLI
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;

            var tesseractDirectoryDatapath = Path.Combine(baseDir, "tessdata");
            var testImagePath = Path.Combine(baseDir, "phototest.tif");
            
            if (args.Length > 0)
            {
                testImagePath = args[0];
            }

            try
            {
                using (var engine = new TesseractEngine(tesseractDirectoryDatapath, "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(testImagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            var text = page.GetText();
                            Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

                            Console.WriteLine("Text (GetText): \r\n{0}", text);
                            Console.WriteLine("Text (iterator):");
                            using (var iter = page.GetIterator())
                            {
                                iter.Begin();

                                // TODO: implement space/tab chars detection in the beginning of line.
                                //int lineCounter = 0;
                                //bool reachFirstWordInLine = false;

                                do
                                {
                                    do
                                    {
                                        do
                                        {
                                            do
                                            {
                                                if (iter.IsAtBeginningOf(PageIteratorLevel.Block))
                                                {
                                                    //lineCounter++;// ???
                                                    //reachFirstWordInLine = false;
                                                    Console.WriteLine("<BLOCK>");
                                                }

                                                //if (!reachFirstWordInLine && string.IsNullOrWhiteSpace(iter.GetText(PageIteratorLevel.Symbol)))
                                                //{
                                                //    Console.Write(" ");
                                                //}

                                                Console.Write(iter.GetText(PageIteratorLevel.Word));
                                                //reachFirstWordInLine = true;
                                                Console.Write(" ");

                                                if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
                                                {
                                                    //lineCounter++;
                                                    //reachFirstWordInLine = false;
                                                    Console.WriteLine();
                                                }
                                            } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));//iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Symbol) ???

                                            if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
                                            {
                                                //lineCounter++;
                                                //reachFirstWordInLine = false;
                                                Console.WriteLine();
                                            }
                                        } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
                                    } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
                                } while (iter.Next(PageIteratorLevel.Block));
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Trace.TraceError(e.ToString());
                Console.WriteLine("Unexpected Error: " + e.Message);
                Console.WriteLine("Details: ");
                Console.WriteLine(e.ToString());
            }
            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}
