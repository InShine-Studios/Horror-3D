using System;

namespace Utils
{
    /*
    * Class that has methods related to items
    */
    public static class ItemHelper
    {
        [Flags]
        public enum UseBehaviourType: short
        {
            None = 1 << 0,
            Environmental = 1 << 1,
            Consumable = 1 << 2,
            Handheld = 1 << 3
        }

        [Flags]
        public enum WorldConditionType : short
        {
            None = 1 << 0,
            Real = 1 << 1,
            Astral = 1 << 2,
            KillPhase = 1 << 3
        }
    }
}
