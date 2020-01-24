using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FileControl
{
    public partial class UserControl1: UserControl
    {
        public string Label { 
            get 
            { 
                return this.label1.Text; 
            }
            set
            {
                string val = string.IsNullOrEmpty(Path.GetFileName(value)) ? value : Path.GetFileName(value);                
                this.label1.Text = StringManipulation.ShortenString(val);
            }
        }

        public Color SelectColor { get; set; }
        public Color TxtColor { get; set; }

        public UserControl1()
        {
            InitializeComponent();
            SelectColor = Color.Chocolate;
            TxtColor = Color.Black;
        }
        
        public UserControl1(string filetype, string fileName) : this()
        {
            Label = fileName;
            if(filetype == "dir")
            {
                pictureBox1.Image = Properties.Resources.Places_folder_red_icon;
            }
            else if(filetype == "file")
            {
                if(Path.GetExtension(fileName) == ".pdf")
                {
                    pictureBox1.Image = Properties.Resources.pdf_icon;
                }
                else if(Path.GetExtension(fileName) == ".dll")
                {
                    pictureBox1.Image = Properties.Resources.Misc_file_dll_icon;
                }
                else if(Path.GetExtension(fileName) == ".png" || Path.GetExtension(fileName) == ".jpg")
                {
                    pictureBox1.Image = Properties.Resources.photos_icon;
                }
                else if(Path.GetExtension(fileName) == ".exe")
                {
                    pictureBox1.Image = Properties.Resources.exe_icon;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.icons8_file_80px;
                }
                
            }
            else if(filetype == "drive")
            {
                pictureBox1.Image = Properties.Resources.Devices_drive_harddisk_icon;
            }
            label1.Text = Label;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            pictureBox1.Click += PictureBox1_Click;
            label1.Click += Label1_Click;            
        }

        private void Label1_Click(object sender, EventArgs e)
        {
            this.BackColor = SelectColor;
            label1.ForeColor = TxtColor;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            this.BackColor = SelectColor;
            label1.ForeColor = TxtColor;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            this.BackColor = SelectColor;
            label1.ForeColor = TxtColor;
        }

        private void UserControl1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
