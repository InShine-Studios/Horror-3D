using System.Collections.Generic;
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
        public enum States
        {
            Dialogue,
            Tome,
            Hiding,
            Exorcism,
            MindMap,
            Default
        }
        public static int FixedUpdateCallsPerSecond = 50;
        public static Dictionary<States,string> PlayerStateActionMapMapper = new Dictionary<States, string>()
        {
            {States.Dialogue, "UI" },
            {States.Tome, "UI" },
            {States.Hiding, "Hiding" },
            {States.Exorcism, "Exorcism" },
            {States.MindMap, "MindMap" },
            {States.Default, "Default" }
        };

        public static bool CheckIsInteractZone(Collider target)
        {
            return target.gameObject.CompareTag("Player") && target.name == "InteractZone";
        }

        public static float DistanceToMoveDuration(IPlayerMovement playerMovement, float distance)
        {
            float speed = playerMovement.GetCurMoveSpeed();
            return (distance / speed) * FixedUpdateCallsPerSecond;
        }
    }
}
