using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FileControl;

namespace Explorer
{
    public partial class Explorer : Form
    {
        private string fullPath;
        private string curFolder;
        private int curSelected;

        public Explorer()
        {
            InitializeComponent();
            fullPath = curFolder = string.Empty;
        }        

        private string[] SetLocation(string location)
        {
            Directory.SetCurrentDirectory(location);
            string loc = Directory.GetCurrentDirectory();
            string[] entries = Directory.GetFileSystemEntries(loc);
            return entries;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // loading all drives
            DriveInfo[] drives = DriveInfo.GetDrives();

            // showing the drives
            foreach(DriveInfo drive in drives)
            {
                if(drive.IsReady)
                {
                    UserControl1 file = new UserControl1("drive", drive.Name);
                    file.Margin = new Padding(20, 20, 0, 20);
                    file.Width = 100;
                    file.Cursor = Cursors.Hand;
                    file.MouseDoubleClick += File_MouseDoubleClick;

                    file.MouseClick += delegate
                    {
                        curFolder = file.Label;
                    };
                    foreach (Control control in file.Controls)
                    {
                        control.MouseDoubleClick += File_MouseDoubleClick;
                        control.MouseClick += delegate
                        {
                            curFolder = file.Label;
                        };
                    }
                    iconsPanel.Controls.Add(file);
                    file.Show();
                }
                
            }

            // setting the path
            txtPath.Text = "Computer";
        }

        private void Show_drives()
        {
            // loading all drives
            DriveInfo[] drives = DriveInfo.GetDrives();

            // showing the drives
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    UserControl1 file = new UserControl1("drive", drive.Name);
                    file.Margin = new Padding(20, 20, 0, 20);
                    file.Width = 100;
                    file.Cursor = Cursors.Hand;
                    file.MouseDoubleClick += File_MouseDoubleClick;
                    file.MouseClick += delegate
                    {
                        curFolder = file.Label;
                    };
                    foreach (Control control in file.Controls)
                    {
                        control.MouseDoubleClick += File_MouseDoubleClick;
                        control.MouseClick += delegate
                        {
                            curFolder = file.Label;
                        };
                    }
                    iconsPanel.Controls.Add(file);
                    file.Show();
                }
                
            }

            // setting the path
            txtPath.Text = "Computer";
        }

        private void Show_dirs(string[] files)
        {
            iconsPanel.Controls.Clear();
            foreach (string fil in files)
            {
                string fileType = Path.HasExtension(fil) ? "file" : "dir";
                UserControl1 file = new UserControl1(fileType, fil);
                file.Margin = new Padding(20, 20, 0, 20);
                file.Width = 100;
                file.ContextMenuStrip = FileMenu;
                file.Cursor = Cursors.Hand;
                file.MouseClick += delegate
                {
                    curFolder = file.Label;
                };
                file.MouseDoubleClick += File_MouseDoubleClick;
                foreach (Control control in file.Controls)
                {
                    control.MouseDoubleClick += File_MouseDoubleClick;
                    control.MouseClick += delegate
                    {
                        curFolder = file.Label;                        
                    };
                }
                iconsPanel.Controls.Add(file);
                file.Show();
            }
            txtPath.Text = curFolder;
        }

        private void Open_File(string path)
        {
            if(Path.GetExtension(path.ToLower()) == ".png" || Path.GetExtension(path.ToLower()) == ".jpg")
            {
                PhotosApp photosApp = new PhotosApp(path);
                photosApp.Show();
                lsPros.Items.Add("PhotoApp: " + path);
                photosApp.FormClosed += delegate
                {
                    lsPros.Items.Remove("PhotoApp: " + path);
                };
            }
            else if(Path.GetExtension(path.ToLower()) == ".txt" 
                || Path.GetExtension(path.ToLower()) == ".cpp"
                || Path.GetExtension(path.ToLower()) == ".c"
                || Path.GetExtension(path.ToLower()) == ".js"
                || Path.GetExtension(path.ToLower()) == ".css"
                || Path.GetExtension(path.ToLower()) == ".html"
                || Path.GetExtension(path.ToLower()) == ".htm"
                || Path.GetExtension(path.ToLower()) == ".py"
                || Path.GetExtension(path.ToLower()) == ".csv"
                || Path.GetExtension(path.ToLower()) == ".cs")
            {
                Editor editor = new Editor(path);
                lsPros.Items.Add("Editor: " + path);
                editor.Show();
                editor.FormClosed += delegate
                {
                    lsPros.Items.Remove("Editor: " + path);
                };
            }
        }

        private void File_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                string[] files = SetLocation(curFolder);
                curFolder = Directory.GetCurrentDirectory();
                Show_dirs(files);
            }
            catch (DirectoryNotFoundException)
            {
                Open_File(curFolder);
            }
            catch(IOException ex)
            {                
                Open_File(curFolder);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            iconsPanel.Controls.Clear();
            Show_drives();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void iconsPanel_Click(object sender, EventArgs e)
        {
            foreach (UserControl1 control in ((FlowLayoutPanel)sender).Controls)
            {
                control.BackColor = Color.Transparent;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            string[] files = SetLocation("..");
            Show_dirs(files);
            txtPath.Text = Directory.GetCurrentDirectory();
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            //string[] files = SetLocation("..");
            //Show_dirs(files);
            //txtPath.Text = Directory.GetCurrentDirectory();
        }

        private void refreshBtn_Click(object sender, EventArgs e)
        {
            iconsPanel.Refresh();
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RenameFile rnm = new RenameFile(curFolder);
            rnm.ShowDialog();
            Show_dirs(SetLocation(curFolder));
        }

        private void linkLabel3_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string[] files = SetLocation(desktopPath);
            
            Show_dirs(files);
        }
    }
}
