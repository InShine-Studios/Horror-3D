using System.Collections;

namespace Astralization.Items.EvidenceItem.ClockStates
{
    public class ClockInitState : ClockState
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(Init());
        }
        #endregion

        #region Initialization
        private IEnumerator Init()
        {
            // set up here for future use
            yield return null;
            owner.ChangeState<ClockInactiveState>();
        }
        #endregion
    }
}