using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YuyukoRecord.game.temp;

namespace YuyukoRecord.game
{
    /// <summary>
    /// 游戏对局文件
    /// </summary>
    internal class GameTempArenaInfo
    {
        private static string REPLAY_PATH = null;
        private string matchGroup;
        private int mapId;
        private List<Vehicles> vehicles;
        private string gameType;
        private string dateTime;
        private string tempArenaInfoJson;

        public static GameTempArenaInfo LoadJson()
        {
            if (string.IsNullOrEmpty(REPLAY_PATH))
            {
                ReplaysPath();
            }
            //检测文件是否存在
            if (File.Exists(REPLAY_PATH))
            {
                FileStream fs = new FileStream(REPLAY_PATH, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
                StringBuilder info = new StringBuilder();
                while (!sr.EndOfStream)
                {
                    info.Append(sr.ReadLine());
                }
                sr.Close();
                fs.Close();
                GameTempArenaInfo game = JsonConvert.DeserializeObject<GameTempArenaInfo>(info.ToString());
                game.TempArenaInfoJson = info.ToString();
                return game;
            }
            return null;
        }

        public string MatchGroup { get => matchGroup; set => matchGroup = value; }
        public int MapId { get => mapId; set => mapId = value; }
        public string GameType { get => gameType; set => gameType = value; }
        public string DateTime { get => dateTime; set => dateTime = value; }
        public List<Vehicles> Vehicles { get => vehicles; set => vehicles = value; }
        public string TempArenaInfoJson { get => tempArenaInfoJson; set => tempArenaInfoJson = value; }

        public static string ReplaysPath()
        {
            if (!string.IsNullOrEmpty(SourcesLoad.LoadGamePath()))
            {
                string replays = "replays";
                string jsonFile = "tempArenaInfo.json";
                FileInfo info = null;
                List<FileInfo> fileInfos = new List<FileInfo>();
                Director(SourcesLoad.LoadGamePath() + replays, fileInfos);
                foreach (FileInfo file in fileInfos)
                {
                    if (file.Name.Contains(jsonFile))
                    {
                        if (info == null || file.LastWriteTimeUtc.CompareTo(info.LastWriteTimeUtc) >= 1)
                        {
                            info = file;
                        }
                    }
                }
                if (info != null && File.Exists(info.FullName))
                {
                    REPLAY_PATH = info.FullName;
                    return REPLAY_PATH;
                }
            }
            return REPLAY_PATH;
        }

        public static void Director(string dir, List<FileInfo> list)
        {
            DirectoryInfo d = new DirectoryInfo(dir);
            FileInfo[] files = d.GetFiles("*.json");
            DirectoryInfo[] directs = d.GetDirectories();
            foreach (FileInfo f in files)
            {
                list.Add(f);
            }
            //获取子文件夹内的文件列表，递归遍历  
            foreach (DirectoryInfo dd in directs)
            {
                Director(dd.FullName, list);
            }
        }
    }
}
