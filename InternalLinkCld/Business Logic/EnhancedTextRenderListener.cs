using System;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf.parser;

namespace InternalLinkCld
{
    public class EnhancedTextRenderListener : ITextExtractionStrategy
    {
        private readonly Rectangle _region;
        private readonly StringBuilder _result = new StringBuilder();
        private Vector _lastEnd;
        private float _yThreshold = 2.0f;

        public EnhancedTextRenderListener(Rectangle region)
        {
            _region = region;
        }

        public void BeginTextBlock() { }

        public void EndTextBlock() { }

        public void RenderText(TextRenderInfo renderInfo)
        {
            var baseline = renderInfo.GetBaseline();
            Vector start = baseline.GetStartPoint();
            Vector end = baseline.GetEndPoint();

            if (start[0] >= _region.Left && end[0] <= _region.Right &&
                start[1] >= _region.Bottom && end[1] <= _region.Top)
            {
                string text = renderInfo.GetText();

                if (_lastEnd != null && Math.Abs(start[1] - _lastEnd[1]) > _yThreshold)
                {
                    _result.AppendLine();
                }

                _result.Append(text);
                _lastEnd = end;
            }
        }

        public string GetResultantText() => _result.ToString().Trim();

        public void RenderImage(ImageRenderInfo renderInfo) { }
    }
}