using Astralization.Items.EvidenceItems;

namespace Astralization.Items.EvidenceItem.ClockStates
{
    public class ClockInactiveState : ClockState, IInactiveState
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            audioInUse = null;
        }
        #endregion
    }
}