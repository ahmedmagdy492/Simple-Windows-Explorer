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
    public partial class RenameFile : Form
    {
        public string FileName { get; set; }

        public RenameFile(string fileName)
        {
            InitializeComponent();
            FileName = fileName;
            txtName.Text = FileName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Path.HasExtension(FileName))
                {
                    string pathOnly = Path.GetFullPath(FileName);
                    string newName = txtName.Text;
                    File.Move(FileName, pathOnly + "\\" + newName);
                }
                else
                {
                    string pathOnly = Path.GetFullPath(FileName);
                    string newName = txtName.Text;
                    Directory.Move(FileName, pathOnly + "\\" + newName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Close();
            }            
        }
    }
}
