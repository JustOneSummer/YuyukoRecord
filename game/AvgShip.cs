using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YuyukoRecord.game
{
    /// <summary>
    /// 服务器平均数据/计算的基点
    /// </summary>
    internal class AvgShip
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private static List<AvgShip> AVG_LIST = new List<AvgShip>();

        private long shipId;
        private double winRate;
        private double averageDamageDealt;
        private double averageFrags;

        public void Add(AvgShip data)
        {
            this.WinRate += data.WinRate;
            this.AverageDamageDealt += data.AverageDamageDealt;
            this.AverageFrags += data.AverageFrags;
        }

        public double AvgDamage(long battle)
        {
            return battle <= 0 ? this.averageDamageDealt : battle * this.averageDamageDealt;
        }

        public double AvgFrags(long battle)
        {
            return battle <= 0 ? this.averageFrags : battle * this.averageFrags;
        }

        public double AvgWins(long battle)
        {
            return battle <= 0 ? this.winRate : battle * this.winRate / 100;
        }

        public static void LoadJson(JToken jt)
        {
            AVG_LIST.Clear();
            foreach (var t in jt)
            {
                AvgShip ship = JsonConvert.DeserializeObject<AvgShip>(t.Value<JToken>("data").ToString());
                ship.ShipId = t.Value<long>("shipId");
                AVG_LIST.Add(ship);
            }
        }

        public static AvgShip GetAvg(long shipId)
        {
           return AVG_LIST.Find(x => x.ShipId == shipId);
        }

        public double WinRate { get => winRate; set => winRate = value; }
        public double AverageDamageDealt { get => averageDamageDealt; set => averageDamageDealt = value; }
        public double AverageFrags { get => averageFrags; set => averageFrags = value; }
        public long ShipId { get => shipId; set => shipId = value; }

        public static void Http()
        {
            string path = System.Environment.CurrentDirectory + "/avg.json";

            //检测文件时间是否超过一天，一天更新一次
            FileInfo fileInfo = new FileInfo(path);
            DateTime lastWriteTimeUtc = fileInfo.LastWriteTimeUtc;
            string lastDate = lastWriteTimeUtc.ToString("yyyy-MM-dd");
            string dayDate = DateTime.UtcNow.ToString("yyyy-MM-dd");
            if (!lastDate.Equals(dayDate))
            {
                try
                {
                    string jsonData = SourcesLoad.Get(SourcesLoad.COS_AVG);
                    using (StreamWriter streamWriter = new StreamWriter(path, false))
                    {
                        streamWriter.WriteLine(jsonData);
                    }
                }
                catch (Exception e)
                {
                    log.Error("请求AVG数据出错！", e);
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
            JToken jt =  JToken.Parse(info.ToString()).Value<JToken>("data");
            LoadJson(jt);
        }
    }
}
