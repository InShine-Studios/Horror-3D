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
            None = 0,
            Environmental = 1 << 0,
            Consumable = 1 << 1,
            Handheld = 1 << 2
        }

        [Flags]
        public enum WorldConditionType : short
        {
            None = 0,
            Real = 1 << 0,
            Astral = 1 << 1,
            KillPhase = 1 << 2
        }
    }
}
