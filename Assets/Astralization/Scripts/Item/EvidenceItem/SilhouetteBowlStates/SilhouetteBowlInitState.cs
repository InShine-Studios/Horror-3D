using System.Collections;

namespace Astralization.Items.EvidenceItem.SilhouetteBowlStates
{
    public class SilhouetteBowlInitState : SilhouetteBowlState
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
            owner.ChangeState<SilhouetteBowlInactiveState>();
        }
        #endregion
    }
}