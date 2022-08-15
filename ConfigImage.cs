using System;
using System.Windows.Forms;
using YuyukoRecord.config;

namespace YuyukoRecord
{
    public partial class ConfigImage : Form
    {
        public ConfigImage()
        {
            InitializeComponent();
        }

        private void ConfigImage_Load(object sender, EventArgs e)
        {
            label2.Text = "图片存储在程序跟目录下面的template文件夹下面的pr文件夹里面";
            label3.Text = "打开文件夹后自行替换同名的图片就行了";
            label4.Text = "请注意图片格式一定要是.jpg格式,否则程序不认的哟!!!";
            if (AppConfigUtils.Instance.ShipImage)
            {
                button1.Text = "禁用";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string paht = @"" + System.Environment.CurrentDirectory;
            System.Diagnostics.Process.Start("explorer.exe", paht);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AppConfigUtils.Instance.ShipImage = button1.Text.Equals("启用");
            AppConfigUtils.Save(AppConfigUtils.Instance);
            Close();
        }
    }
}
