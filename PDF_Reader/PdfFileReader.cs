using iText;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

namespace PDF_Reader;

public class PdfFileReader
{
    public string ReadPDF(string path)
    {
        string content = null;
        PdfReader pdfReader = new(path);
        PdfDocument pdfDocument = new(pdfReader);

        for (int pageCount = 1; pageCount <= pdfDocument.GetNumberOfPages(); pageCount++)
        {
            ITextExtractionStrategy textExtraction = new SimpleTextExtractionStrategy();
            content += PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(pageCount), textExtraction);
        }

        pdfDocument.Close();
        pdfReader.Close();
        return content;
    }
}