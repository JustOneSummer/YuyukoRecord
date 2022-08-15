namespace YuyukoRecord.game.data
{
    internal class GameAvg
    {
        private int count;
        private int pr;
        private double wins;
        private int battle;

        public int AvgPr()
        {
            return (int)(Pr / (double)Count);
        }

        public double AvgWins()
        {
            return wins / count;
        }

        public int Count { get => count; set => count = value; }
        public int Pr { get => pr; set => pr = value; }
        public double Wins { get => wins; set => wins = value; }
        public int Battle { get => battle; set => battle = value; }

    }
}
