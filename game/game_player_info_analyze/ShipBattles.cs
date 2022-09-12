﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YuyukoRecord.game.game_player_info_analyze
{
    internal class ShipBattles
    {
        /**
    * 战舰ID
    */
        private long shipId;
        /**
         * 战斗场次
         */
        private long battles;
        /**
         * 胜利场次
         */
        private long wins;
        /**
         * 存活场次
         */
        private long survivedBattles;

        /**
         * 伤害
         */
        private long damage;
        /**
         * 经验
         */
        private long xp;

        /**
         * 总击杀
         */
        private long frags;

        /**
         * 炮弹命中次数
         */
        private long hit;

        /**
         * 发射炮弹次数
         */
        private long shots;

        public static ShipBattles ToData(JToken token,long shipId)
        {
            ShipBattles info = new ShipBattles();
            info.ShipId = shipId;
            if (token == null)
            {
                return info;
            }
            info.Battles = token["battles_count"].Value<long>();
            info.Wins = token["wins"].Value<long>();
            info.SurvivedBattles = token["survived"].Value<long>();
            info.Damage = token["damage_dealt"].Value<long>();
            info.Xp = token["premium_exp"].Value<long>();
            info.Frags = token["frags"].Value<long>();
            info.Hit = token["hits_by_main"].Value<long>();
            info.Shots = token["shots_by_main"].Value<long>();
            return info;
        }

        public long ShipId { get => shipId; set => shipId = value; }
        public long Battles { get => battles; set => battles = value; }
        public long Wins { get => wins; set => wins = value; }
        public long SurvivedBattles { get => survivedBattles; set => survivedBattles = value; }
        public long Damage { get => damage; set => damage = value; }
        public long Xp { get => xp; set => xp = value; }
        public long Frags { get => frags; set => frags = value; }
        public long Hit { get => hit; set => hit = value; }
        public long Shots { get => shots; set => shots = value; }
    }
}
