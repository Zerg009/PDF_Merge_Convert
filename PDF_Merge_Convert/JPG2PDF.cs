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
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
namespace PDF_Merge_Convert
{
    public partial class JPG2PDF : Form
    {
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
                var path = fileDialog.FileNames[0];
                int index = path.LastIndexOf('\\');
                PdfDocument pdfdocument = new PdfDocument();
                
                newDirectoryPath = path.Substring(0, index + 1) + "JPG2PDF(" + DateTime.Now.ToString("h:mm:ss").Replace(':', '_')+")";
                // 1. Compress selected images and store them in new TEMP folder
                String tempFolderPath = path.Substring(0, index + 1) + "TempImages";
                DirectoryInfo d = Directory.CreateDirectory(tempFolderPath); //Assuming Test is your Folder
                d.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                foreach (var file in fileDialog.FileNames)
                {
                    int indexFile = file.LastIndexOf('\\');
                    Image img = Image.FromFile(file);
                    Image imgPhoto;
                    long size = new System.IO.FileInfo(file).Length;
                    if (size >= 2000000)
                    {
                        imgPhoto = ScaleByPercent(img, 50);
                    }else if(size >= 1000000)
                    {
                        imgPhoto = ScaleByPercent(img, 75);
                    }
                    else
                    {
                        imgPhoto = img;
                    }
                    int len = file.Length - indexFile;
                    String FileName = file.Substring(indexFile + 1, len - 1);
                    String outputPath = tempFolderPath + "\\" + "reduced_size_" + FileName;
                    imgPhoto.Save(outputPath, ImageFormat.Jpeg);
                    imgPhoto.Dispose();
                    
                }

                // 2. Take all the compressed file and make the pdf
                string[] Files = GetAllFiles(tempFolderPath,"*.jpeg|*.jpg.|*.png",SearchOption.TopDirectoryOnly); //Getting Text 

                foreach (var file in Files)
                {
                    //Image img = Image.FromFile(file);
                    PdfPage page = pdfdocument.AddPage();
                    using (XImage image = XImage.FromFile(file))
                    {


                        double wid_inches = image.PixelWidth / image.HorizontalResolution;
                    double heig_inches = image.PixelHeight / image.HorizontalResolution;

                    if (image.PixelWidth < image.PixelHeight)
                        page.Orientation = PageOrientation.Portrait;
                    else
                        page.Orientation = PageOrientation.Landscape;
                    page.Width = wid_inches*72;
                    page.Height = heig_inches * 72;
                    XGraphics gfx = XGraphics.FromPdfPage(page);
                    gfx.DrawImage(image, 0, 0, page.Width, page.Height);
                    
                    }
                }
                // Save
                pdfdocument.Save(newDirectoryPath+".pdf");
                pdfdocument.Close();
                label1.Text = "Finished.";
                MessageBox.Show("Merging finished succesfully.\n File is located at "+ newDirectoryPath+".pdf");
                if (Directory.Exists(tempFolderPath))
                {
                    Directory.Delete(tempFolderPath,true);
                }
            }
            else
            {
                MessageBox.Show("You din not select the files!\n Select the files to convert.", "Select files error.");
            }

            
        }
        private Image ScaleByPercent(Image imgPhoto, int Percent)
        {
            float nPercent = ((float)Percent / 100);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight,
                                     PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                                    imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }
        private static string[] GetAllFiles(string sourceFolder, string filters, System.IO.SearchOption searchOption)
        {
            return filters.Split('|').SelectMany(filter => System.IO.Directory.GetFiles(sourceFolder, filter, searchOption)).ToArray();
        }
        private Image Compress(Image img)
        {
            //DirectoryInfo d = new DirectoryInfo(newDirectoryPath); //Assuming Test is your Folder

            //string[] Files = GetAllFiles(newDirectoryPath,"*.jpeg|*.jpg.|*.png",SearchOption.TopDirectoryOnly); //Getting Text 

            //foreach (var file in Files)
            //{
            //    int index = file.LastIndexOf('\\');

            //    Image img = Image.FromFile(file);
                Image imgPhoto = ScaleByPercent(img, 50);
            //    int len = file.Length - index;
            /*    String FileName = file.Substring(index + 1, len - 1);
                String outputPath = newDirectoryPath + "reduced_size_" + FileName;
                imgPhoto.Save(outputPath, ImageFormat.Jpeg);
                imgPhoto.Dispose();
            */
            //}
            return img;
            
        }
    }
}
