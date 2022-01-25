using UnityEngine;

namespace Utils
{
    /*
    * Class that has methods related to player
    */
    public static class ColorParser
    {
        private static Color _color;

        public static Color GetColor(string hex)
        {
            bool bConverted = ColorUtility.TryParseHtmlString(hex, out _color);
            if (bConverted) return _color;
            else return Color.black;
        }
    }
}
