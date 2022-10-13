using Newtonsoft.Json.Linq;
using YuyukoRecord.game.data;

namespace YuyukoRecord.game.game_player_info_analyze
{
    internal class ApiShipAll
    {
        public static GameUser All(long shipId,string shipAll,long accountId)
        {
            JToken token = JToken.Parse(shipAll);
            JToken shipList = token["data"][accountId]["statistics"];
            GameInfoPrAvgData si = new GameInfoPrAvgData();
            GameInfoPrAvgData total = new GameInfoPrAvgData();
            foreach (var content in shipList)
            {
                JProperty jProperty = content.ToObject<JProperty>();
                GameInfoPrAvgData gipad = new GameInfoPrAvgData(ShipBattles.ToData(content, long.Parse(jProperty.Name)));
                if(gipad.Ship.ShipId == shipId)
                {
                    si = gipad;
                }
                total.Add(gipad);
            }
            GameUser user = new GameUser();
            user.AccountId = accountId;
            user.Pvp = new GamePlayerInfo(total);
            user.Ship = new GamePlayerInfo(si);
            return user;
        }
    }
}
