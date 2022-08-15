using System;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace YuyukoRecord
{
    public partial class UpdateFile : Form
    {
        public UpdateFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            label2.Text = "开始下载资源文件...";
            string home = System.Environment.CurrentDirectory + "\\";
            string path = home + "ships.zip";
            SourcesLoad.GetFile("http://v.wows.shinoaki.com/ships.zip", path);
            label2.Text = "解压资源文件中......";
            string ships = home + "ships";
            FileInfo fileInfo = new FileInfo(path);
            if (File.Exists(path) && fileInfo.Length >= 1000)
            {
                File.Delete(ships);
            }
            ZipFile.ExtractToDirectory(path, home);
            Close();
        }

        private void UpdateFile_Load(object sender, EventArgs e)
        {

        }
    }
}
