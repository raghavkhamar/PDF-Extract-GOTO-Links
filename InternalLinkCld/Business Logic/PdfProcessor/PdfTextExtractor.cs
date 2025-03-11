using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace InternalLinkCld
{
    public class PdfTextExtractor : ITextExtractor
    {
        public string ExtractText(PdfReader reader, int pageNum, Rectangle region)
        {
            var filters = new RenderFilter[] { new RegionTextRenderFilter(region) };
            var strategy = new FilteredTextRenderListener(new EnhancedTextRenderListener(region), filters);
            return iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, pageNum, strategy).Trim();
        }
    }
}