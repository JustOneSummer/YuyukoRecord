using System;
using System.Windows.Forms;

namespace YuyukoRecord
{
    public partial class ConfigGamePath : Form
    {
        public ConfigGamePath()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dilog = new FolderBrowserDialog();
            dilog.Description = "请选择游戏根目录";
            DialogResult dialogResult = dilog.ShowDialog();
            if (dialogResult == DialogResult.OK || dialogResult == DialogResult.Yes)
            {
                textBox1.Text = dilog.SelectedPath;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SourcesLoad.WriterGamePath(textBox1.Text + "\\");
            Close();
        }

        private void ConfigGamePath_Load(object sender, EventArgs e)
        {

        }
    }
}
