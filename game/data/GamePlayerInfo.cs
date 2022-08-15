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
