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
using ImageMagick;
using Syroot.Windows.IO;
namespace PDF_Merge_Convert
{
    public partial class HEIC_JPG : Form
    {
        OpenFileDialog fileDialog = new OpenFileDialog();
        public HEIC_JPG()
        {
            InitializeComponent();
            MagickNET.Initialize();
            Init_FileDialog();
        }

        private void OpenFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
        private void Init_FileDialog()
        {
            string downloadsPath = new KnownFolder(KnownFolderType.Downloads).Path;
            fileDialog.InitialDirectory = downloadsPath;
            fileDialog.Title = "Select Heic files.";
            fileDialog.DefaultExt = "heic";
            fileDialog.Filter = "Image Files|*.heic";
            fileDialog.Multiselect = true;
        }
        private void BrowseBtn_Click(object sender, EventArgs e)
        {
            fileDialog.ShowDialog();
            foreach (String file in fileDialog.FileNames)
            {
                int index = file.LastIndexOf('\\')+1;
                int len = file.Length;
                MessageBox.Show(file.Substring(index,len-index));
            }

        }

        private void ConvertBtn_Click(object sender, EventArgs e)
        {
            String path = "C:\\Users\\semen\\source\\repos\\PDF_Merge_Convert2\\images\\";
            string[] allfiles = Directory.GetFiles(path, "*.heic", SearchOption.AllDirectories);
            String newDirectoryPath = path + "HEIC_JPG_" + DateTime.Now.ToString("h:mm:ss").Replace(':', '_');
            // vipsimage.WriteToFile(path+"sample11.jpg");
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

            foreach (var file in allfiles)
            {
                FileInfo info = new FileInfo(file);
                int found = info.Name.IndexOf(".heic");
                String outputPath = newDirectoryPath + "\\" + info.Name.Substring(0, found) + ".jpg";
                if (!File.Exists(path))
                {
                    using (MagickImage image = new MagickImage(info.FullName))
                    {
                        // Save frame as jpg
                        image.Write(outputPath);
                    }
                }
            }
        }
    }
}
