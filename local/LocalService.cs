using System;
using System.Collections.Generic;
using System.Threading;
using YuyukoRecord.game;
using YuyukoRecord.game.data;
using YuyukoRecord.game.temp;

namespace YuyukoRecord.local
{
    internal class LocalService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static Queue<GameUser> QUEUE = new Queue<GameUser>();

        public static void Put(GameUser user)
        {
            QUEUE.Enqueue(user);
        }

        public static GameUser Poll()
        {
            if (QUEUE.Count > 0)
            {
                return QUEUE.Dequeue();
            }
            return null;
        }

        public static void LoadGameInfo(string server, GameData gameData)
        {
            //上传地图信息
            try
            {
                Dictionary<string, string> map = new Dictionary<string, string>();
                map.Add("server", server);
                map.Add("mapJson", gameData.GameTempArenaInfo.TempArenaInfoJson);
                string result = WowsHttp.PostJson(WowsHttp.HOME + "/public/wows/parse/upload/map", map);
                log.Info("上传地图信息结果="+result);
            }
            catch(Exception ex)
            {
                log.Error("上传地图信息失败!" + ex);
            }
            int botId = -1;
            //本地化计算
            List< Vehicles> vehicles = new List< Vehicles>();  
            foreach (var temp in gameData.GameTempArenaInfo.Vehicles)
            {
                if (temp.Name.IndexOf(":") >= 0)
                {
                    GameUser user = new GameUser();
                    user.UserName = temp.Name;
                    user.AccountId = botId;
                    user.Hide = true;
                    Put(user);
                }
                else
                {
                    vehicles.Add(temp);
                    
                }           
            }
            AvgList(vehicles, 4).ForEach(x=> ThreadPool.QueueUserWorkItem(state =>
            {
                x.ForEach(m =>
                {
                    log.Info(server + " 查询用户=" + m.Name);
                    //查询
                    Put(WowsHttp.ShinoAki("asia", m));
                });
            }));
        
        }


        public static List<List<Vehicles>> AvgList(List<Vehicles> data, int core)
        {
            int avg;
            if (data.Count <= core)
            {
                core = data.Count;
                avg = 1;
            }
            else
            {
                avg = data.Count / core;
            }
            List<List<Vehicles>> list = new List<List<Vehicles>>(core);
            int cursor = 0;
            int i = 0;
            while (true)
            {
                i++;
                //判断是否是最后一位
                if (i < core)
                {
                    int tem = cursor;
                    cursor += avg;
                    list.Add(data.GetRange(tem, avg));
                }
                else
                {
                    list.Add(data.GetRange(cursor, avg));
                    return list;
                }
            }
        }
    }
}
