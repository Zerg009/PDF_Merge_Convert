using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syroot.Windows.IO;
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace PDF_Merge_Convert
{
    public partial class JPG2PDF : Form
    {
        int page_num = -1;
        readonly OpenFileDialog fileDialog = new OpenFileDialog();

        public JPG2PDF()
        {
            InitializeComponent();
            Init_FileDialog();
        }
        private void Init_FileDialog()
        {
            string downloadsPath = new KnownFolder(KnownFolderType.Downloads).Path;
            fileDialog.InitialDirectory = downloadsPath;
            fileDialog.Title = "Select JPG/PNG files.";
            fileDialog.DefaultExt = "jpg";
            fileDialog.Filter = "Image Files|*.jpg;*.png";
            fileDialog.Multiselect = true;
        }
        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            fileDialog.ShowDialog();
        }

        private void JPG2PDF_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            PdfDocument pdfdocument = new PdfDocument();
            foreach (var file in fileDialog.FileNames)
            {
                // Add to pdf all the files from FileDialog
                PdfPage page = pdfdocument.AddPage();
                // temp incrementing
                page_num++;
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XImage image = XImage.FromFile(file);
                if (image.PixelWidth<image.PixelHeight)
                {
                    page.Orientation = PageOrientation.Portrait;
                    //gfx.DrawImageRotated(image, 0, 0);
                    gfx.DrawImage(image, 0, 0);
                }
                else
                {
                    page.Orientation = PageOrientation.Landscape;
                    gfx.DrawImage(image, 0, 0);

                }
                // Save and start View

            }
            pdfdocument.Save(@"C:\Users\semen\source\repos\PDF_Merge_Convert2"+"\\new_pdf.pdf");
        }
    }
}
