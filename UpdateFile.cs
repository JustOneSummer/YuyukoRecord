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
            DownLoadFile("http://v.wows.shinoaki.com/ships.zip", "ships.zip", new Action<int, int>((m, v) => {
                this.progressBar1.Maximum = m;
                this.progressBar1.Value = v;
            }));
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


        public static bool DownLoadFile(string URL, string Filename, Action<int, int> updateProgress = null)
        {
            Stream st = null;
            Stream so = null;
            System.Net.HttpWebRequest Myrq = null;
            System.Net.HttpWebResponse myrp = null;
            bool flag = false;
            try
            {
                Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                //从WEB响应得到总字节数
                long totalBytes = myrp.ContentLength;
                //更新进度
                if (updateProgress != null)
                {
                    //从总字节数得到进度条的最大值
                    updateProgress((int)totalBytes, 0);
                }
                st = myrp.GetResponseStream();
                so = new System.IO.FileStream(Filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    //更新文件大小 
                    totalDownloadedByte = osize + totalDownloadedByte;
                    Application.DoEvents();
                    so.Write(by, 0, osize);
                    //更新进度
                    if (updateProgress != null)
                    {
                        updateProgress((int)totalBytes, (int)totalDownloadedByte);//更新进度条 
                    }
                    //读流 
                    osize = st.Read(by, 0, (int)by.Length);
                }
                //更新进度
                if (updateProgress != null)
                {
                    updateProgress((int)totalBytes, (int)totalBytes);
                }
                flag = true;
            }
            catch (Exception)
            {
                flag = false;
                throw;
                //return false;
            }
            finally
            {
                if (Myrq != null)
                {
                    Myrq.Abort();//销毁关闭连接
                }
                if (myrp != null)
                {
                    myrp.Close();//销毁关闭响应
                }
                if (so != null)
                {
                    so.Close(); //关闭流 
                }
                if (st != null)
                {
                    st.Close(); //关闭流  
                }
            }
            return flag;
        }
    }
}
