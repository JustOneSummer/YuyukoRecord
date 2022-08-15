namespace YuyukoRecord.game.temp
{
    internal class Vehicles
    {
        private long accountId;
        private long shipId;
        private int relation;
        private string name;

        public long AccountId { get => accountId; set => accountId = value; }
        public long ShipId { get => shipId; set => shipId = value; }
        public int Relation { get => relation; set => relation = value; }
        public string Name { get => name; set => name = value; }
    }
}
