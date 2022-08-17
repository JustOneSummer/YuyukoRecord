using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using YuyukoRecord.Properties;

namespace YuyukoRecord.game
{
    public class ShipCache : IComparable<ShipCache>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static Dictionary<long, ShipCache> SHIP_MAP = new Dictionary<long, ShipCache>();

        private long id;
        private string shipNameCn;
        private string name;
        private string country;
        private string shipType;
        private int tier;
        private string shipIdValue;

        public static void LoadJson(string json)
        {
            SHIP_MAP.Clear();
            JsonConvert.DeserializeObject<List<ShipCache>>(json).ForEach(s =>
            {
                if (!SHIP_MAP.ContainsKey(s.Id))
                {
                    SHIP_MAP.Add(s.Id, s);
                }
            });
            log.Info("加载 [" + SHIP_MAP.Count + "] 战舰信息缓存 size=" + SHIP_MAP.Count);
        }

        public static ShipCache GetMap(long id)
        {
            var ship = SHIP_MAP[id];
            if (ship == null)
            {
                ship = new ShipCache();
                ship.ShipNameCn = "未知战舰";
            }
            return ship;
        }

        public static Image GetImage(string shipIndex)
        {
            string path = System.Environment.CurrentDirectory + "\\ships\\" + shipIndex + ".png";
            if (File.Exists(path))
            {
                return Image.FromFile(path);
            }
            return Resources._default;
        }


        public string ShipNameCn { get => shipNameCn; set => shipNameCn = value; }
        public string Name { get => name; set => name = value; }
        public string Country { get => country; set => country = value; }
        public string ShipType { get => shipType; set => shipType = value; }
        public int Tier { get => tier; set => tier = value; }
        public string ShipIdValue { get => shipIdValue; set => shipIdValue = value; }
        public long Id { get => id; set => id = value; }

        public int CompareTo(ShipCache other)
        {
            if (!shipType.Equals(other.ShipType))
            {
                return ShipType.CompareTo(other.ShipType);
            }
            else if (tier != other.Tier)
            {
                return other.Tier.CompareTo(Tier);
            }
            else if (!shipIdValue.Equals(other.shipIdValue))
            {
                return shipIdValue.CompareTo(other.shipIdValue);
            }
            return 0;
        }


        public static void Http()
        {
            string path = System.Environment.CurrentDirectory + "/ship.json";

            //检测文件时间是否超过一天，一天更新一次
            FileInfo fileInfo = new FileInfo(path);
            DateTime lastWriteTimeUtc = fileInfo.LastWriteTimeUtc;
            string lastDate = lastWriteTimeUtc.ToString("yyyy-MM-dd");
            string dayDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            if (!lastDate.Equals(dayDate))
            {
                try
                {
                    string jsonData = SourcesLoad.Get(SourcesLoad.URL + "/public/wows/encyclopedia/ship/search");
                    using (StreamWriter streamWriter = new StreamWriter(path, false))
                    {
                        streamWriter.WriteLine(jsonData);
                    }
                }
                catch (Exception e)
                {
                    log.Error("请求ship数据出错！", e);
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
