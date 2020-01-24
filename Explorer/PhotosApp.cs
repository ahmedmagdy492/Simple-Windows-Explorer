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

namespace Explorer
{
    public partial class PhotosApp : Form
    {
        public PhotosApp()
        {
            InitializeComponent();
        }

        public PhotosApp(string fullPath) : this()
        {
            FullPath = fullPath;
            ImgName = Path.GetFileName(fullPath);
            pictureBox1.Image = Image.FromFile(FullPath);
            this.Text = FullPath;
        }

        public string FullPath { get; set; }
        public string ImgName { get; set; }
    }
}
