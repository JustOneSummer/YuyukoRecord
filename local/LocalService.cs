using System.Collections.Generic;
using YuyukoRecord.game;
using YuyukoRecord.game.data;
using YuyukoRecord.game.temp;

namespace YuyukoRecord.local
{
    internal class LocalService
    {
       private static Queue<GameUser> QUEUE = new Queue<GameUser>();

        public static void Put(GameUser user)
        {
            QUEUE.Enqueue(user);
        }

        public static GameUser Poll()
        {
            return QUEUE.Dequeue();
        }

        public static void LoadGameInfo(string server, GameTempArenaInfo gameTempArenaInfo)
        {
            int botId = -1;
           foreach(var temp in gameTempArenaInfo.Vehicles)
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
                    //查询
                }
            }
        }

        private static void Query(string server, Vehicles vehicles)
        {

        }
    }
}
