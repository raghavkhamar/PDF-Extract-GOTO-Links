using System;
using System.Windows.Forms;

namespace InternalLinkCld
{
    public class ConsoleUserInterface
    {
        public string SelectFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*",
                FilterIndex = 1,
                Title = "Select a PDF File"
            };

            return openFileDialog.ShowDialog() == DialogResult.OK ? openFileDialog.FileName : null;
        }

        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}