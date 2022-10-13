namespace YuyukoRecord.game.game_player_info_analyze
{
    internal class GameInfoPrAvgData
    {
        private ShipBattles ship;
        private int battle;
        private AvgShip avg;

        public GameInfoPrAvgData( )
        {
            this.ship = new ShipBattles();
            this.battle = 0;
            this.avg = new AvgShip();
        }


        public GameInfoPrAvgData(ShipBattles ship)
        {
            this.ship = ship;
            this.battle = ship.Battles;
            this.avg = AvgShip.GetAvg(ship.ShipId);
        }

        public void Add(GameInfoPrAvgData data)
        {
            this.Ship.Add(data.Ship);
            this.battle += data.Battle;
            this.Avg.Add(data.Avg);
        }

        public int Battle { get => battle; set => battle = value; }
            internal ShipBattles Ship { get => ship; set => ship = value; }
            internal AvgShip Avg { get => avg; set => avg = value; }
        }
}
