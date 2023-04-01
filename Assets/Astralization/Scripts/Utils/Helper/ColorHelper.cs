using UnityEngine;

namespace Astralization.Utils.Helper
{
    /*
    * Class that has methods related to player
    */
    public static class ColorHelper
    {
        private static Color _color;

        public static Color ParseHex(string hex)
        {
            bool bConverted = ColorUtility.TryParseHtmlString(hex, out _color);
            if (bConverted) return _color;
            else
            {
                Debug.LogError("[UTILS] ColorHelper is unable to parse from given hex code");
                return Color.black;
            }
        }
    }
}
