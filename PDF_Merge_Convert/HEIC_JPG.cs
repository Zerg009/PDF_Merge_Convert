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
        readonly OpenFileDialog fileDialog = new OpenFileDialog();
        
        String newDirectoryPath;
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
                //MessageBox.Show(file.Substring(index,len-index));
            }

        }

        private void ConvertBtn_Click(object sender, EventArgs e)
        {
            label1.Text = "Waiting for conversion...";
            label1.Refresh();
            String path = fileDialog.FileNames[0];
            int index=path.LastIndexOf('\\');
            newDirectoryPath = path.Substring(0,index+1) + "HEIC_JPG_" + DateTime.Now.ToString("h:mm:ss").Replace(':', '_');
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

            Parallel.ForEach(fileDialog.FileNames, file =>
            {
                FileInfo info = new FileInfo(file);
                int found = info.Name.IndexOf(".heic");
                if (found == -1)
                {
                    found = info.Name.IndexOf(".HEIC");
                }
                String outputPath = newDirectoryPath + "\\" + info.Name.Substring(0, found) + ".jpg";
                if (!File.Exists(outputPath))
                {
                    using (MagickImage image = new MagickImage(info.FullName))
                    {
                        // Save frame as jpg
                        image.Write(outputPath);
                    }
                }
            });
            label1.Text = "Finished!";
            MessageBox.Show("Converting finished!\nCheck:" + newDirectoryPath, "Finished");
        }

    }
}