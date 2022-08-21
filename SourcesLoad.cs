using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Net;
using System.Text;
using YuyukoRecord.utils;

namespace YuyukoRecord
{
    /// <summary>
    /// 资源加载类
    /// </summary>
    internal class SourcesLoad
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static string URL = "https://api.wows.shinoaki.com";
        public static string COS_PR = "https://yuyuko-1253221348.cos.ap-shanghai.myqcloud.com/app/api/PR.json";
        public static string COS_SHIP_INFO = "https://yuyuko-1253221348.cos.ap-shanghai.myqcloud.com/app/api/ship_info_data.json";
        private static string GAME_PATH = System.Environment.CurrentDirectory + "/game_path.txt";
        private static string GAME_HOME = null;
        private static string GAME_SERVER = null;

        public static string LoadGamePath()
        {
            if (string.IsNullOrEmpty(GAME_HOME))
            {
                FileInfo fileInfo = new FileInfo(GAME_PATH);
                if (fileInfo.Exists)
                {
                    FileStream fs = new FileStream(GAME_PATH, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
                    GAME_HOME = sr.ReadLine();
                    sr.Close();
                    fs.Close();
                }
            }
            return GAME_HOME;
        }

        public static void WriterGamePath(string path)
        {
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(GAME_PATH, false))
                {
                    streamWriter.WriteLine(path);
                }
            }
            catch (Exception e)
            {
                log.Error("写入路径文件出错！", e);
            }
        }

        public static string GetVersion()
        {
            try
            {
                Get(URL + "/tools/yuyuko/upload/cpuid?info=" + GetCpuID());
                return Get(URL + "/tools/yuyuko/version");
            }
            catch (Exception e)
            {
                log.Error("版本相关信息获取异常 : ", e);
            }
            return "0.0.1";
        }

        public static string Get(string url)
        {
            try
            {
                System.GC.Collect();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json;charset=UTF-8";
                request.Timeout = 20000;
                request.KeepAlive = false;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string encoding = response.Headers["content-encoding"];
                string retString;
                if (encoding != null && "gzip".Equals(encoding))
                {
                    retString = GzipUtils.Decompress(response);
                }
                else
                {

                    StreamReader myStreamReader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
                    retString = myStreamReader.ReadToEnd();
                    myStreamReader.Close();
                }
                response.Close();
                return retString;
            }
            catch (Exception e)
            {
                log.Error(url + " 请求失败!" + e.Message);
            }
            return null;
        }

        public static void GetFile(string url, string path)
        {
            try
            {
                System.GC.Collect();
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                Stream stream = new FileStream(path, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, bArr.Length);
                }
                stream.Close();
                responseStream.Close();
            }
            catch (Exception ex)
            {
                log.Error("下载文件失败!" + ex.Message);
            }
        }

        public static string GetCpuID()
        {
            try
            {
                string cpuInfo = "";
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo = mo.Properties["ProcessorId"].Value.ToString();
                }
                moc = null;
                mc = null;
                return cpuInfo;
            }
            catch
            {
                return "unknow";
            }

            finally { }
        }

        public static void removeLog()
        {
            string path = System.Environment.CurrentDirectory + "/logs/yuyuko.log";

            //检测文件时间是否超过一天，一天更新一次
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Exists)
            {
                if (fileInfo.Length >= 104857600)
                {
                    fileInfo.Delete();
                }
            }
        }

        /// <summary>
        /// 获取是那个服务器的
        /// </summary>
        /// <returns></returns>
        public static string ServerInfo()
        {
            if (string.IsNullOrEmpty(GAME_SERVER))
            {
                //读取解析服务器信息
                string logFile = GAME_HOME + "profile/clientrunner.log";
                if (!File.Exists(logFile))
                {
                    log.Error("profile/clientrunner.log 不存在...");
                    return GAME_HOME;
                }
                FileStream fs = new FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
                string info = null;
                List<string> list = new List<string>();
                while (!sr.EndOfStream)
                {
                    list.Add(sr.ReadLine());
                }
                sr.Close();
                fs.Close();
                for (int i = list.Count - 1; i > 0; i--)
                {
                    string tem = list[i];
                    if (tem.Contains("Selected realm"))
                    {
                        info = tem.Substring(tem.LastIndexOf("realm") + 6);
                        break;
                    }
                }
                GAME_SERVER = info.Trim();
            }
            return GAME_SERVER;
        }
    }
}
