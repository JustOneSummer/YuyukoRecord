using System;
using System.Collections.Generic;
using YuyukoRecord.game;
using YuyukoRecord.game.data;

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
           foreach(var temp in gameData.GameTempArenaInfo.Vehicles)
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
                    log.Info(server + " 查询用户="+temp.Name);
                    //查询
                    Put(WowsHttp.ShinoAki(server, temp));
                }
            }
        }
    }
}
