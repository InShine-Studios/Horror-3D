namespace Astralization.States.TimeslotStates
{
    public class TimeslotMorningState : TimeslotState
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            timeNum = 0;
            timeName = "Morning";
        }
        #endregion
    }
}