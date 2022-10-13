using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using YuyukoRecord.game.data;

namespace YuyukoRecord.game.game_player_info_analyze
{
    internal class ApiShipAll
    {
        public static GameUser All(string shipAll,long accountId)
        {
            JToken token = JToken.Parse(shipAll);
            JToken shipList = token["data"][accountId]["statistics"];
            
            Dictionary<long, ShipBattles> shipBattlesMap = new Dictionary<long, ShipBattles>();
            foreach (var content in shipList)
            {
                JProperty jProperty = content.ToObject<JProperty>();
                long shipId = long.Parse(jProperty.Name);
                shipBattlesMap.Add(shipId, ShipBattles.ToData(content, shipId));
            }
            //筛选计算 计算总战绩和单船的战绩
            return null;
        }
    }
}
