using Astralization.Items.EvidenceItems;

namespace Astralization.Items.EvidenceItem.SilhouetteBowlStates
{
    public class SilhouetteBowlInactiveState : SilhouetteBowlState, IInactiveState
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
        }
        #endregion

        #region Handler
        public override void Enter()
        {
            base.Enter();
            positiveModel.SetActive(false);
            negativeModel.SetActive(false);
        }
        #endregion
    }
}