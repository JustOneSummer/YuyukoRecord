using Newtonsoft.Json.Linq;
using YuyukoRecord.game.data;

namespace YuyukoRecord.game.game_player_info_analyze
{
    internal class ApiShipAll
    {
        public static GameUser All(long shipId,string shipAll,long accountId)
        {
            JToken token = JToken.Parse(shipAll);
            /*JToken t1 = token.Value<JToken>("data");
            JToken t2 = t1.Value<JToken>(accountId.ToString());
            JToken t3 = t2.Value<JToken>("statistics");*/
            JToken shipList = token.Value<JToken>("data").Value<JToken>(accountId.ToString()).Value<JToken>("statistics");
            GameInfoPrAvgData si = new GameInfoPrAvgData();
            GameInfoPrAvgData total = new GameInfoPrAvgData();
            foreach (var content in shipList)
            {
                JProperty jProperty = content.ToObject<JProperty>();
                string lsShipId = jProperty.Name;
                GameInfoPrAvgData gipad = new GameInfoPrAvgData(ShipBattles.ToData(content, long.Parse(lsShipId)));
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
