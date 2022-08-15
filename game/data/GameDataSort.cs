using System;

namespace YuyukoRecord.game.data
{
    internal class GameDataSort : IComparable<GameDataSort>
    {
        private string userName;
        private ShipCache shipCache;

        public GameDataSort(string userName, ShipCache shipCache)
        {
            this.userName = userName;
            this.shipCache = shipCache;
        }

        public string UserName { get => userName; set => userName = value; }
        public ShipCache ShipCache { get => shipCache; set => shipCache = value; }

        public int CompareTo(GameDataSort other)
        {
            return shipCache.CompareTo(other.shipCache);
        }
    }
}
