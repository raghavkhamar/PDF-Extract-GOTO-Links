using System;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace InternalLinkCld
{
    public interface ITextExtractor
    {
        string ExtractText(PdfReader reader, int pageNum, Rectangle region);
    }

    public interface IAnnotationProcessor
    {
        void ProcessAnnotation(PdfReader reader, int pageNum, PdfDictionary annotation, Action<string> onTextExtracted);
    }

    public interface ILinkExtractor
    {
        void ExtractLinks(string pdfPath, Action<string> onLinkExtracted);
    }
}