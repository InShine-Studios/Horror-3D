using Astralization.Items.EvidenceItems;

namespace Astralization.Items.EvidenceItem.SilhouetteBowlStates
{
    public class SilhouetteBowlPositiveState : SilhouetteBowlState, IPositiveState
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
            positiveModel.SetActive(true);
            negativeModel.SetActive(false);
        }
        #endregion
    }
}