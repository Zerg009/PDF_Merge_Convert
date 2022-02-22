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
            fileDialog.Filter = "Image Files|*.jpg;*.png;*.jpeg";
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
            String newDirectoryPath="";
            if ((fileDialog.FileNames).Length!=0)
            {
                label1.Text = "Merging images, please wait...";
                label1.Refresh();
                PdfDocument pdfdocument = new PdfDocument();
                var path = fileDialog.FileNames[0];
                int index = path.LastIndexOf('\\');
                newDirectoryPath = path.Substring(0, index + 1) + "JPG2PDF(" + DateTime.Now.ToString("h:mm:ss").Replace(':', '_')+")";

                foreach (var file in fileDialog.FileNames)
                {
                    // Add to pdf all the files from FileDialog
                    PdfPage page = pdfdocument.AddPage();
                    // temp incrementing
                    page_num++;
                    XImage image = XImage.FromFile(file);
                    double wid_inches = image.PixelWidth / image.HorizontalResolution;
                    double heig_inches = image.PixelHeight / image.HorizontalResolution;

                    if (image.PixelWidth < image.PixelHeight)
                        page.Orientation = PageOrientation.Portrait;
                    else
                        page.Orientation = PageOrientation.Landscape;
                    
                    page.Width = wid_inches*72;
                    page.Height = heig_inches*72;
        
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    

                    gfx.DrawImage(image, 0, 0, page.Width, page.Height);
                }
                // Save
                pdfdocument.Save(newDirectoryPath+".pdf");
                label1.Text = "Finished.";
                MessageBox.Show("Merging finished succesfully.\n File is located at "+ newDirectoryPath+".pdf");
            }
            else
            {
                MessageBox.Show("You din not select the files!\n Select the files to convert.", "Select files error.");
            }
        }
    }
}
