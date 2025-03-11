using System;
using iTextSharp.text;
using iTextSharp.text.pdf;
using InternalLinkCld.Business_Logic;

namespace InternalLinkCld
{
    public class LinkAnnotationProcessor : IAnnotationProcessor
    {
        private readonly ITextExtractor _textExtractor;
        private readonly PdfLinkChecker _linkChecker;

        public LinkAnnotationProcessor(ITextExtractor textExtractor, PdfLinkChecker linkChecker)
        {
            _textExtractor = textExtractor;
            _linkChecker = linkChecker;
        }

        public void ProcessAnnotation(PdfReader reader, int pageNum, PdfDictionary annotation, Action<string> onTextExtracted)
        {
            if (annotation == null || !_linkChecker.IsInternalLink(annotation)) return;

            var rectArray = annotation.GetAsArray(PdfName.RECT);
            if (rectArray == null || rectArray.Size != 4) return;

            const float margin = 0.5f;

            var region = new Rectangle(
                rectArray.GetAsNumber(0).FloatValue,
                rectArray.GetAsNumber(1).FloatValue,
                rectArray.GetAsNumber(2).FloatValue + margin,
                rectArray.GetAsNumber(3).FloatValue
            );

            string extractedText = _textExtractor.ExtractText(reader, pageNum, region);
            if (!string.IsNullOrEmpty(extractedText))
            {
                onTextExtracted.Invoke($"Text: {extractedText} || Source Page: {pageNum}");
            }
        }
    }
}