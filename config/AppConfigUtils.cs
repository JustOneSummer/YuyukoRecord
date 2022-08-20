using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace YuyukoRecord.config
{
    internal class AppConfigUtils
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static AppConfigUtils instance;
        /// <summary>
        /// 是否使用自定义图片
        /// </summary>
        private bool shipImage = false;
        /// <summary>
        /// 模板类型
        /// </summary>
        private int templateType = 0;

        /// <summary>
        /// 默认mq服务关闭
        /// </summary>
        private bool mqttServer = false;

        public static void LoadInit()
        {
            string path = System.Environment.CurrentDirectory + "\\config.json";
            ApiConfig.LoadInit();
            if (File.Exists(path))
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
                StringBuilder info = new StringBuilder();
                while (!sr.EndOfStream)
                {
                    info.Append(sr.ReadLine());
                }
                sr.Close();
                fs.Close();
                string json = info.ToString();
                log.Info("加载配置文件: " + json);
                Instance = JsonConvert.DeserializeObject<AppConfigUtils>(json);
            }
            else
            {
                Instance = new AppConfigUtils();
                Save(Instance);
            }
        }

        public static void Save(AppConfigUtils utils)
        {
            instance = utils;
            string jsonData = JsonConvert.SerializeObject(utils, Newtonsoft.Json.Formatting.Indented);
            try
            {
                string path = System.Environment.CurrentDirectory + "\\config.json";
                using (StreamWriter streamWriter = new StreamWriter(path, false))
                {
                    streamWriter.WriteLine(jsonData);
                }
                log.Info("配置文件保存成功!" + jsonData);
            }
            catch (Exception e)
            {
                log.Error("保存配置文件失败！", e);
            }
        }

        public bool ShipImage { get => shipImage; set => shipImage = value; }
        public int TemplateType { get => templateType; set => templateType = value; }
        internal static AppConfigUtils Instance { get => instance; set => instance = value; }
        public bool MqttServer { get => mqttServer; set => mqttServer = value; }
    }
}
