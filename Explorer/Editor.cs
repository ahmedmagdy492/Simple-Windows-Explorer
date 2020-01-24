using System.Windows.Forms;
using System.IO;

namespace Explorer
{
    public partial class Editor : Form
    {
        public string FilePath { get; set; }
        private string buffer;
        private string originalData;

        public Editor()
        {
            InitializeComponent();
        }

        public Editor(string fileName) : this()
        {
            FilePath = fileName;
            buffer = string.Empty;
            originalData = string.Empty;
        }

        private void Editor_Load(object sender, System.EventArgs e)
        {
            this.Width = Screen.PrimaryScreen.Bounds.Width - 200;
            originalData = File.ReadAllText(FilePath);
            string Data = originalData;
            txtData.Text = Data;
            this.Text = FilePath;
        }

        private void txtData_TextChanged(object sender, System.EventArgs e)
        {
            buffer = txtData.Text;
        }

        private void Editor_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;
            if(buffer != originalData)
            {
                result = MessageBox.Show("You made some changes, would you like to save? ", "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                if(result == DialogResult.Yes)
                {
                    File.WriteAllText(FilePath, buffer);
                }
                else if(result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void fontSettingsToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            using (FontDialog fd = new FontDialog())
            {
                if(fd.ShowDialog() == DialogResult.OK)
                {
                    txtData.Font = fd.Font;
                }
            }
        }
    }
}
