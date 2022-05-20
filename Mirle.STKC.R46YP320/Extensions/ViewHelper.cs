using Mirle.MPLC.DataType;
using System.Drawing;

namespace Mirle.STKC.R46YP320.Extensions
{
    public static class ViewHelper
    {
        public static Color ToColor(this Bit signal)
        {
            return signal.IsOn() ? Color.Yellow : Color.White;
        }

        public static Color ToColor(this bool signal)
        {
            return signal ? Color.Yellow : Color.White;
        }

        public static Color ToButtonColor(this Bit signal)
        {
            return signal.IsOn() ? Color.Lime : Color.Gainsboro;
        }

        public static Color ToCmdButtonColor(this Bit signal)
        {
            return signal.IsOn() ? Color.Lime : Color.DarkGray;
        }
    }
}