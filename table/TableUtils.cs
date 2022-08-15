using System;
using System.Drawing;
using System.Windows.Forms;
using YuyukoRecord.game;
using YuyukoRecord.game.data;

namespace YuyukoRecord.table
{
    internal class TableUtils
    {
        public const string USER_NAME = "玩家";
        public const string PR = "评分";
        public const string BATTLE = "胜率/场均";
        public const string SHIP = "战舰";

        public static void LoadGame(DataGridView view, GameData info)
        {
            TableTemplate.One(view, info);
        }

        public static void LoadGame(DataGridView view, GameUser info)
        {
            TableTemplate.Two(view, info);
        }


        public static void Load(DataGridView view)
        {
            DataGridViewImageColumn columnOne = new DataGridViewImageColumn();
            columnOne.ImageLayout = DataGridViewImageCellLayout.Zoom;
            DataGridViewImageColumn columnTwo = new DataGridViewImageColumn();
            columnTwo.ImageLayout = DataGridViewImageCellLayout.Zoom;
            view.RowHeadersVisible = false;
            view.Columns.Add("USER_NAME_ONE", USER_NAME);
            view.Columns["USER_NAME_ONE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            view.Columns.Add("BATTLE_ONE", BATTLE);
            view.Columns["BATTLE_ONE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            view.Columns.Add("PR_ONE", PR);
            view.Columns["PR_ONE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            view.Columns.Add("SHIP_LEVEL_ONE", SHIP);
            view.Columns["SHIP_LEVEL_ONE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            view.Columns.Add("SHIP_BATTLE_ONE", BATTLE);
            view.Columns["SHIP_BATTLE_ONE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            view.Columns.Add("SHIP_PR_ONE", PR);
            view.Columns["SHIP_PR_ONE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            view.Columns.Add(columnOne);
            view.Columns.Add(columnTwo);

            view.Columns.Add("SHIP_PR_TWO", PR);
            view.Columns["SHIP_PR_TWO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            view.Columns.Add("SHIP_BATTLE_TWO", BATTLE);
            view.Columns["SHIP_BATTLE_TWO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            view.Columns.Add("SHIP_LEVEL_TWO", SHIP);
            view.Columns["SHIP_LEVEL_TWO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            view.Columns.Add("PR_TWO", PR);
            view.Columns["PR_TWO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            view.Columns.Add("BATTLE_TWO", BATTLE);
            view.Columns["BATTLE_TWO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;


            view.Columns.Add("USER_NAME_TWO", USER_NAME);
            view.Columns["USER_NAME_TWO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //view.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //view.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            view.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            view.AllowUserToAddRows = false;
            //view.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            view.ClearSelection();
            //view.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            Hw(view);
        }


        /// <summary>
        /// 界面行宽
        /// </summary>
        public static void Hw(DataGridView view)
        {
            view.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            view.Columns[0].FillWeight = 30;
            view.Columns[1].FillWeight = 10;
            view.Columns[2].FillWeight = 8;
            view.Columns[3].FillWeight = 14;
            view.Columns[4].FillWeight = 10;
            view.Columns[5].FillWeight = 8;
            view.Columns[6].FillWeight = 8;

            view.Columns[7].FillWeight = 8;
            view.Columns[8].FillWeight = 8;
            view.Columns[9].FillWeight = 10;
            view.Columns[10].FillWeight = 14;
            view.Columns[11].FillWeight = 8;
            view.Columns[12].FillWeight = 10;
            view.Columns[13].FillWeight = 30;

            for (int i = 0; i < view.Columns.Count; i++)
            {
                view.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                view.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            for (int i = 0; i < view.Rows.Count; i++)
            {
                view.Rows[i].Height = 70;
                view.Rows[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                if (i == 0)
                {
                    view.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(Convert.ToInt32("ffF8F8FF", 16));
                }
            }

        }
    }
}
