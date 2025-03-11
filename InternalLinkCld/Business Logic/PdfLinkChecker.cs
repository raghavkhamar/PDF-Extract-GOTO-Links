using iTextSharp.text.pdf;

namespace InternalLinkCld.Business_Logic
{
    public class PdfLinkChecker
    {
        public bool IsInternalLink(PdfDictionary annotation)
        {
            if (!annotation.Get(PdfName.SUBTYPE).Equals(PdfName.LINK)) return false;
            var action = annotation.GetAsDict(PdfName.A);
            return action?.Get(PdfName.S).Equals(PdfName.GOTO) ??
                   annotation.Contains(PdfName.DEST);
        }
    }
}