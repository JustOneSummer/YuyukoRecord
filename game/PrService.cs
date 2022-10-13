using System;
using YuyukoRecord.game.game_player_info_analyze;

namespace YuyukoRecord.game
{
    internal class PrService
    {

        public static int Pr(GameInfoPrAvgData data)
        {
            double nd = data.Ship.Damage / data.Avg.AvgDamage(data.Battle);
            double nf = data.Ship.Frags / data.Avg.AvgFrags(data.Battle);
            double nw = data.Ship.Wins / data.Avg.AvgWins(data.Battle);
            return Result(nd, nf, nw);
        }

        private static int Result(double nd, double nf, double nw)
        {
            double maxNd = Math.Max(0, (nd - 0.4) / (1 - 0.4));
            double maxNf = Math.Max(0, (nf - 0.1) / (1 - 0.1));
            double maxNw = Math.Max(0, (nw - 0.7) / (1 - 0.7));
            return (int)Math.Floor(700 * maxNd + 300 * maxNf + 150 * maxNw);
        }

        public static int number(double data)
        {
            return (int)Math.Ceiling(data);
        }

        /**
     * 场均击杀
     *
     * @param battles 战斗场次
     * @param frags   击杀数
     * @return 结果
     */
        public static double frags(long battles, long frags)
        {
            return doubleCheck(((double)frags) / battles);
        }


        /**
         * 场均
         *
         * @param battles     场次
         * @param damageDealt 场均
         * @return 场均
         */
        public static double damage(long battles, long damageDealt)
        {
            if (damageDealt <= 0 || battles <= 0)
            {
                return 0;
            }
            return doubleCheck(damageDealt / battles);
        }

        /**
         * 胜率
         *
         * @param wins    胜利场次
         * @param battles 场次
         * @return 胜率
         */
        public static double wins(long battles, double wins)
        {
            if (wins <= 0 || battles <= 0)
            {
                return 0;
            }
            return 100.0 * doubleCheck((wins / battles));
        }

        /**
         * 经验
         *
         * @param xp      总经验
         * @param battles 场次
         * @return 经验
         */
        public static double xp(long battles, double xp)
        {
            if (xp <= 0 || battles <= 0)
            {
                return 0;
            }
            return doubleCheck(xp / battles);
        }

        /**
         * KD
         *
         * @param battles         场次
         * @param frags           击杀
         * @param survivedBattles 存活场次
         * @return kd
         */
        public static double kd(long battles, double frags, double survivedBattles)
        {
            double v = battles - survivedBattles;
            if (frags <= 0)
            {
                return 0;
            }
            else if (v <= 0)
            {
                return frags;
            }
            else
            {
                return doubleCheck(frags / v);
            }
        }

        /**
         * 命中
         *
         * @param hit   命中
         * @param shots 发射
         * @return 命中率
         */
        public static double hit(double hit, double shots)
        {
            if (hit <= 0 || shots <= 0)
            {
                return 0;
            }
            return 100.0 * doubleCheck((hit / shots));
        }

        public static double doubleCheck(double data)
        {
            if (double.IsInfinity(data))
            {
                return 0;
            }
            else if (double.IsNaN(data))
            {
                return 0;
            }
            else
            {
                return data;
            }
        }
    }
}
