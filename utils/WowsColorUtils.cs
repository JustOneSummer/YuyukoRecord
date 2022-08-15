using System.Drawing;
using YuyukoRecord.table;

namespace YuyukoRecord.utils
{
    internal class WowsColorUtils
    {

        /// <summary>
        /// 颜色区分
        /// </summary>
        /// <param name="wins"></param>
        /// <returns></returns>
        public static Color WinsColor(double wins)
        {
            Color color = TableTemplate.ColorRGB("#ff6e66");
            if (wins <= 40.0)
            {
                return color;
            }
            else if (wins <= 48.0)
            {
                color = TableTemplate.ColorRGB("#ffc51a");
            }
            else if (wins <= 55.0)
            {
                color = TableTemplate.ColorRGB("#4ecc00");
            }
            else if (wins <= 60.0)
            {
                color = TableTemplate.ColorRGB("#da70f5");
            }
            else
            {
                color = TableTemplate.ColorRGB("#c111ee");
            }
            return color;
        }
    }
}
