using YuyukoRecord.game.game_player_info_analyze;

namespace YuyukoRecord.game.data
{
    internal class GamePlayerInfo
    {
        private int pr;
        private int battles;
        private double wins;
        private int damage;
        private int xp;
        private double kd;
        private double hit;
        private double frags;


        public GamePlayerInfo()
        {

        }

        public GamePlayerInfo(GameInfoPrAvgData data)
        {
            ShipBattles ship = data.Ship;
            this.pr = PrService.Pr(data);
            this.battles = ship.Battles;
            this.wins = PrService.wins(ship.Battles, ship.Wins);
            this.damage = PrService.number(PrService.damage(ship.Battles, ship.Damage));
            this.xp = PrService.number(PrService.xp(ship.Battles, ship.Xp));
            this.kd = PrService.kd(ship.Battles,ship.Frags, ship.SurvivedBattles);
            this.hit = PrService.hit(ship.Battles, ship.Shots);
            this.frags = PrService.frags(ship.Battles, ship.Frags);
        }


        public int Pr { get => pr; set => pr = value; }
        public int Battles { get => battles; set => battles = value; }
        public double Wins { get => wins; set => wins = value; }
        public int Damage { get => damage; set => damage = value; }
        public int Xp { get => xp; set => xp = value; }
        public double Kd { get => kd; set => kd = value; }
        public double Hit { get => hit; set => hit = value; }
        public double Frags { get => frags; set => frags = value; }
    }
}
