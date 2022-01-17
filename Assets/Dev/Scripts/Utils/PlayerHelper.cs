using UnityEngine;

namespace Utils
{
    /*
    * Class that has methods related to player
    */
    public static class PlayerHelper
    {
        public enum Direction
        {
            Down,
            Up,
            Right,
            Left
        }

        public static bool CheckIsInteractZone(Collider target)
        {
            return target.gameObject.CompareTag("Player") && target.name == "InteractZone";
        }
    }
}
