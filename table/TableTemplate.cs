using System;
using System.Drawing;
using System.Windows.Forms;
using YuyukoRecord.game;
using YuyukoRecord.game.data;
using YuyukoRecord.utils;

namespace YuyukoRecord.table
{
    internal class TableTemplate
    {
        public static string RN = Environment.NewLine;
        private static Font YH = new Font("微软雅黑", 12, FontStyle.Bold);
        public static void One(DataGridView view, GameData data)
        {
            for (int i = 0; i < data.One.Count; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(view);
                row.Cells[0].Value = data.One[i].UserName;
                row.Cells[3].Value = data.One[i].ShipCache.Tier.ToString() + RN + data.One[i].ShipCache.ShipNameCn;


                row.Cells[6].Value = ShipCache.GetImage(data.One[i].ShipCache.ShipIdValue);
                Image image = ShipCache.GetImage(data.Two[i].ShipCache.ShipIdValue);
                image.RotateFlip(RotateFlipType.Rotate180FlipY);
                row.Cells[7].Value = image;
                row.Cells[10].Value = data.Two[i].ShipCache.Tier.ToString() + RN + data.Two[i].ShipCache.ShipNameCn;
                row.Cells[13].Value = data.Two[i].UserName;
                view.Rows.Add(row);
            }
            TableUtils.Hw(view);
        }

        public static void Two(DataGridView view, GameUser data)
        {
            foreach (DataGridViewRow row in view.Rows)
            {
                if (data.MyTeam)
                {
                    My(row, data);
                }
                else
                {
                    My2(row, data);
                }
            }
            TableUtils.Hw(view);
        }

        private static void My(DataGridViewRow row, GameUser data)
        {
            if (data.UserName.Equals(row.Cells[0].Value))
            {
                row.Cells[0].Style.Font = YH;
                row.Cells[2].Style.Font = YH;
                row.Cells[5].Style.Font = YH;
                if (!string.IsNullOrEmpty(data.ClanTag))
                {
                    row.Cells[0].Value = "[" + data.ClanTag + "]" + data.UserName;
                    row.Cells[0].Style.ForeColor = ColorRGB(data.ClanColor);
                }
                row.Cells[1].Value = Info(data.Hide,data.Pvp.Battles,data.Pvp.Wins,data.Pvp.Damage);
                row.Cells[1].Style.ForeColor = WowsColorUtils.WinsColor(data.Pvp.Wins);
                PrCache pr = PrCache.GetList(data.Pvp.Pr);
                row.Cells[2].Value = data.Pvp.Pr;
                row.Cells[2].Style.ForeColor = ColorRGB(pr.Color);
                //ship
                row.Cells[4].Value = Info(data.Hide, data.Ship.Battles, data.Ship.Wins, data.Ship.Damage);
                row.Cells[4].Style.ForeColor = WowsColorUtils.WinsColor(data.Ship.Wins);
                PrCache shipPr = PrCache.GetList(data.Ship.Pr);
                row.Cells[5].Value = data.Ship.Pr;
                row.Cells[5].Style.ForeColor = ColorRGB(shipPr.Color);
            }
        }

        private static void My2(DataGridViewRow row, GameUser data)
        {
            if (data.UserName.Equals(row.Cells[13].Value))
            {
                row.Cells[13].Style.Font = YH;
                row.Cells[11].Style.Font = YH;
                row.Cells[8].Style.Font = YH;
                if (!string.IsNullOrEmpty(data.ClanTag))
                {
                    row.Cells[13].Value = data.UserName + "[" + data.ClanTag + "]";
                    row.Cells[13].Style.ForeColor = ColorRGB(data.ClanColor);
                }

                //场次/胜率
                row.Cells[12].Value = Info(data.Hide, data.Pvp.Battles, data.Pvp.Wins, data.Pvp.Damage);
                row.Cells[12].Style.ForeColor = WowsColorUtils.WinsColor(data.Pvp.Wins);
                PrCache pr = PrCache.GetList(data.Pvp.Pr);
                row.Cells[11].Value = data.Pvp.Pr;
                row.Cells[11].Style.ForeColor = ColorRGB(pr.Color);
                //ship
                row.Cells[9].Value = Info(data.Hide, data.Ship.Battles, data.Ship.Wins, data.Ship.Damage);
                row.Cells[9].Style.ForeColor = WowsColorUtils.WinsColor(data.Ship.Wins);
                PrCache shipPr = PrCache.GetList(data.Ship.Pr);
                row.Cells[8].Value = data.Ship.Pr;
                row.Cells[8].Style.ForeColor = ColorRGB(shipPr.Color);
            }
        }

        public static Color ColorRGB(string rgb)
        {
            return ColorTranslator.FromHtml(rgb);
        }

        private static string Info(bool hide, int battle, double wins, int damage)
        {
            return hide ? "隐藏战绩" : battle + "场" + RN + wins + "%" + RN + damage;
        }
    }
}
