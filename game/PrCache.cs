using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YuyukoRecord.game
{
    public class PrCache : IComparable<PrCache>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static List<PrCache> PR_LIST = new List<PrCache>();

        private int code;
        private int value;
        private int nextValue;
        private string name;
        private string englishName;
        private string color;

        public static void LoadJson(string json)
        {
            PR_LIST.Clear();
            PR_LIST.AddRange(JsonConvert.DeserializeObject<List<PrCache>>(json));
        }

        public static PrCache GetList(int value)
        {
            if (value <= 0)
            {
                return PR_LIST[0];
            }
            foreach (var pr in PR_LIST)
            {
                if (value < pr.value)
                {
                    return pr;
                }
            }
            return PR_LIST[PR_LIST.Count - 1];
        }

        public int CompareTo(PrCache other)
        {
            return code.CompareTo(other.code);
        }

        public int Code { get => code; set => code = value; }
        public int Value { get => value; set => this.value = value; }
        public int NextValue { get => nextValue; set => nextValue = value; }
        public string Name { get => name; set => name = value; }
        public string EnglishName { get => englishName; set => englishName = value; }
        public string Color { get => color; set => color = value; }

        public static void Http()
        {
            string path = System.Environment.CurrentDirectory + "/pr.json";

            //检测文件时间是否超过一天，一天更新一次
            FileInfo fileInfo = new FileInfo(path);
            DateTime lastWriteTimeUtc = fileInfo.LastWriteTimeUtc;
            string lastDate = lastWriteTimeUtc.ToString("yyyy-MM-dd");
            string dayDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            if (!lastDate.Equals(dayDate))
            {
                try
                {
                    string jsonData = SourcesLoad.Get(SourcesLoad.URL + "/public/wows/encyclopedia/pr/list");
                    using (StreamWriter streamWriter = new StreamWriter(path, false))
                    {
                        streamWriter.WriteLine(jsonData);
                    }
                }
                catch (Exception e)
                {
                    log.Error("请求PR数据出错！", e);
                }
            }

            FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.UTF8);
            StringBuilder info = new StringBuilder();
            while (!sr.EndOfStream)
            {
                info.Append(sr.ReadLine());
            }
            sr.Close();
            fs.Close();
            LoadJson(JToken.Parse(info.ToString()).Value<JToken>("data").ToString());
        }
    }
}
