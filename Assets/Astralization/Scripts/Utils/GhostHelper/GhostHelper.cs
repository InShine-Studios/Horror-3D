namespace Utils
{
    /*
    * Class that has methods related to ghost property
    */
    public static class GhostHelper
    {
        #region Line of Sight
        public enum GhostLineOfSightType
        {
            Long,
            Average,
            Ground,
            None
        }
        #endregion

        #region Hearing
        public enum GhostHearingRadiusType
        {
            Short,
            Average,
            Long
        }
        #endregion

        #region Capture
        public enum GhostCaptureZoneType
        {
            Spotlight,
            AOE_Sphere,
            In_Front
        }
        public enum GhostCaptureReachType
        {
            Ground,
            Average,
            Long
        }
        public enum GhostCaptureDurationType
        {
            Instant,
            Delay
        }
        #endregion

        #region Movement
        public enum GhostMovementType
        {
            Walk,
            Flies,
            Teleport
        }

        public enum GhostMovementSpeedType
        {
            Slow,
            Average
        }

        public enum GhostChaseType
        {
            Run,
            Flying,
            Charge,
            None
        }

        public enum GhostChaseSpeedType
        {
            Fast,
            Average,
            None
        }
        #endregion
    }
}
