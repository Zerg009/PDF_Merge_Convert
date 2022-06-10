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
namespace PDF_Merge_Convert
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // MagickNET.Initialize();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            HEIC_JPG form1 = new HEIC_JPG();
            form1.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            JPG2PDF form2 = new JPG2PDF();
            form2.Show();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Doc_to_PDF form3 = new Doc_to_PDF();
            form3.Show();
        }
    }
}
