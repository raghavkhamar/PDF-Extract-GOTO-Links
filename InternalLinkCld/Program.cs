using System;
using InternalLinkCld;
using InternalLinkCld.Business_Logic;

public static class Program
{
    [STAThread]
    public static void Main()
    {
        var ui = new ConsoleUserInterface();
        string filePath = ui.SelectFile();

        if (string.IsNullOrEmpty(filePath))
        {
            ui.DisplayMessage("No file selected. Exiting...");
            return;
        }

        var textExtractor = new PdfTextExtractor();
        var linkChecker = new PdfLinkChecker();
        var annotationProcessor = new LinkAnnotationProcessor(textExtractor,linkChecker);
        var linkExtractor = new PdfLinkExtractor(annotationProcessor);

        linkExtractor.ExtractLinks(filePath, link => ui.DisplayMessage(link));

        ui.DisplayMessage("Processing complete. Press any key to exit...");
        Console.ReadKey();
    }
}