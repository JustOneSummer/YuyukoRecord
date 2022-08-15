using System.Collections.Generic;
using YuyukoRecord.game.data;

namespace YuyukoRecord.game
{
    internal class GameData
    {
        private GameTempArenaInfo gameTempArenaInfo;
        private List<GameUser> gameUserList = new List<GameUser>();
        private List<GameDataSort> one;
        private List<GameDataSort> two;
        private GameAvg avgOne = new GameAvg();
        private GameAvg avgTwo = new GameAvg();

        public void Process()
        {
            int oneCount = 0;
            int onePr = 0;
            double oneWins = 0;
            int oneBattle = 0;

            int twoCount = 0;
            int twoPr = 0;
            double twoWins = 0;
            int twoBattle = 0;
            foreach (GameUser user in gameUserList)
            {
                if (user.Pvp.Battles >= 1)
                {
                    if (user.MyTeam)
                    {
                        oneCount++;
                        onePr += user.Pvp.Pr;
                        oneWins += user.Pvp.Wins;
                        oneBattle += user.Pvp.Battles;
                    }
                    else
                    {
                        twoCount++;
                        twoPr += user.Pvp.Pr;
                        twoWins += user.Pvp.Wins;
                        twoBattle += user.Pvp.Battles;
                    }
                }
            }
            avgOne.Count = oneCount;
            avgOne.Pr = onePr;
            avgOne.Wins = oneWins;
            avgOne.Battle = oneBattle;

            avgTwo.Count = twoCount;
            avgTwo.Pr = twoPr;
            avgTwo.Wins = twoWins;
            avgTwo.Battle = twoBattle;
        }

        public static GameData ToData(GameTempArenaInfo gameTempArenaInfo)
        {
            GameData data = new GameData();
            data.gameTempArenaInfo = gameTempArenaInfo;
            data.one = new List<GameDataSort>();
            data.two = new List<GameDataSort>();
            gameTempArenaInfo.Vehicles.ForEach(v =>
            {
                var ship = ShipCache.GetMap(v.ShipId);
                if (v.Relation <= 1)
                {
                    data.one.Add(new GameDataSort(v.Name, ship));
                }
                else
                {
                    data.two.Add(new GameDataSort(v.Name, ship));
                }
            });
            return data;
        }


        internal GameTempArenaInfo GameTempArenaInfo { get => gameTempArenaInfo; set => gameTempArenaInfo = value; }
        internal List<GameUser> GameUserList { get => gameUserList; set => gameUserList = value; }
        internal List<GameDataSort> One { get => one; set => one = value; }
        internal List<GameDataSort> Two { get => two; set => two = value; }
        internal GameAvg AvgOne { get => avgOne; set => avgOne = value; }
        internal GameAvg AvgTwo { get => avgTwo; set => avgTwo = value; }
    }
}
