using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smarrtCalculator
{
    public static class AppSettings
    {
        public static bool IsDarkTheme { get; set; } = false;
        public static int FontSize { get; set; } = 10;
    }
    public static class Helper
    {
        public static void SetFontSize(Control parent, int size)
        {
            foreach (Control ctrl in parent.Controls)
            {
                ctrl.Font = new Font(ctrl.Font.FontFamily, size);
                if (ctrl.HasChildren)
                    SetFontSize(ctrl, size);
            }
        }
    }

}
