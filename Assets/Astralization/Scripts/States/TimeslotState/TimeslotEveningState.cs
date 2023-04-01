namespace Astralization.States.TimeslotStates
{
    public class TimeslotEveningState : TimeslotState
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            timeNum = 2;
            timeName = "Evening";
        }
        #endregion
    }
}