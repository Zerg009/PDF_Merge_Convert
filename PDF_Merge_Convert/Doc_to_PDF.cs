using Syroot.Windows.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Aspose.Words;
using WordToPDF;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;

namespace PDF_Merge_Convert
{
    public partial class Doc_to_PDF : Form
    {
        readonly OpenFileDialog fileDialog = new OpenFileDialog();
        private void Init_FileDialog()
        {
            string downloadsPath = new KnownFolder(KnownFolderType.Downloads).Path;
            fileDialog.InitialDirectory = downloadsPath;
            fileDialog.Title = "Select your files.";
            fileDialog.DefaultExt = "doc";
            fileDialog.Filter = "Documents Files|*.doc;*.docx;*rtf";
            fileDialog.Multiselect = true;
        }
        public Doc_to_PDF()
        {
            Init_FileDialog();
            InitializeComponent();
        }

        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            fileDialog.ShowDialog();
        }
        private static void MergeMultiplePDFIntoSinglePDF(string outputFilePath, string[] pdfFiles)
        {
            Console.WriteLine("Merging started.....");
            PdfDocument outputPDFDocument = new PdfDocument();
            foreach (string pdfFile in pdfFiles)
            {
                PdfDocument inputPDFDocument = PdfReader.Open(pdfFile, PdfDocumentOpenMode.Import);
                outputPDFDocument.Version = inputPDFDocument.Version;
                foreach (PdfPage page in inputPDFDocument.Pages)
                {
                    outputPDFDocument.AddPage(page);
                }
            }
            outputPDFDocument.Save(outputFilePath);
            Console.WriteLine("Merging Completed");
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            var pdfList = new List<string>();
            String path = fileDialog.FileNames[0];
            int index = path.LastIndexOf('\\');
            string[] pdfFiles;
            string newDirectoryPath = path.Substring(0, index + 1) + "DOC_PDF_" + DateTime.Now.ToString("h:mm:ss").Replace(':', '_');
            String outputPath="";
            label1.Text = "Files are selected!";
            try
            {
                // Determine whether the directory exists.
                if (Directory.Exists(newDirectoryPath))
                {
                    MessageBox.Show("That path exists already.");
                    return;
                }

                // Try to create the directory.
                DirectoryInfo di = Directory.CreateDirectory(newDirectoryPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The process failed: {0}", ex.ToString());
            }

            // Converting selected files

            foreach(var file in fileDialog.FileNames )
            {
                FileInfo info = new FileInfo(file);
                int found = info.Name.IndexOf(".doc");
                if (found == -1)
                {
                    found = info.Name.IndexOf(".rtf");
                }

                outputPath = newDirectoryPath + "\\" + info.Name.Substring(0, found) + ".pdf";
                Document doc = new Document(file);
                doc.Save(outputPath);
                pdfList.Add(outputPath);
            }

            pdfFiles = pdfList.ToArray();

            // Convert to one file
            if (checkBox1.Checked)
            {
                outputPath = newDirectoryPath + "\\" + "Your PDF" + ".pdf";
                PdfDocument outputPDFDocument = new PdfDocument();
                foreach (string pdfFile in pdfFiles)
                {
                    PdfDocument inputPDFDocument = PdfReader.Open(pdfFile, PdfDocumentOpenMode.Import);
                    outputPDFDocument.Version = inputPDFDocument.Version;
                    foreach (PdfPage page in inputPDFDocument.Pages)
                    {
                        outputPDFDocument.AddPage(page);
                    }
                }
                foreach(var file in pdfFiles)
                {
                    File.Delete(file);
                }
                outputPDFDocument.Save(outputPath);
            }
            label1.Text = "Finished!";
            MessageBox.Show("Converting finished!\nCheck:" + newDirectoryPath, "Finished");
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Doc_to_PDF_Load(object sender, EventArgs e)
        {

        }

    }
}
