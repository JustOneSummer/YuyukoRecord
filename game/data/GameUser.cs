namespace YuyukoRecord.game.data
{
    internal class GameUser
    {
        private bool myTeam;
        private bool hide;
        private long accountId;
        private string userName;
        private string clanTag;
        private string clanColor;
        private GamePlayerInfo pvp;
        private GamePlayerInfo ship;

        public bool MyTeam { get => myTeam; set => myTeam = value; }
        public bool Hide { get => hide; set => hide = value; }
        public long AccountId { get => accountId; set => accountId = value; }
        public string UserName { get => userName; set => userName = value; }
        public string ClanTag { get => clanTag; set => clanTag = value; }
        public string ClanColor { get => clanColor; set => clanColor = value; }
        public GamePlayerInfo Pvp { get => pvp; set => pvp = value; }
        public GamePlayerInfo Ship { get => ship; set => ship = value; }
    }
}
