using System;
using System.Collections.Generic;
using iTextSharp.text.pdf;
using InternalLinkCld;

public class PdfLinkExtractor : ILinkExtractor
{
    private readonly IAnnotationProcessor _annotationProcessor;

    public PdfLinkExtractor(IAnnotationProcessor annotationProcessor)
    {
        _annotationProcessor = annotationProcessor;
    }

    public void ExtractLinks(string pdfPath, Action<string> onLinkExtracted)
    {
        var reader = new PdfReader(pdfPath);

        for (int pageNum = 1; pageNum <= reader.NumberOfPages; pageNum++)
        {
            var annotations = GetAnnotations(reader, pageNum);
            if (annotations == null) continue;

            foreach (var annotObj in annotations)
            {
                var annotation = (PdfDictionary)PdfReader.GetPdfObject(annotObj);
                _annotationProcessor.ProcessAnnotation(reader, pageNum, annotation, onLinkExtracted);
            }
        }
    }

    private IEnumerable<PdfObject> GetAnnotations(PdfReader reader, int pageNum)
    {
        var pageDict = reader.GetPageN(pageNum);
        return pageDict.GetAsArray(PdfName.ANNOTS)?.ArrayList;
    }
}