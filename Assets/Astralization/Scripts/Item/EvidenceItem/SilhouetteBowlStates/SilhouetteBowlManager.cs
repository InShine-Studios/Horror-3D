using Astralization.SPI;
using System.Collections.Generic;

namespace Astralization.Items.EvidenceItem.SilhouetteBowlStates
{
    public class SilhouetteBowlManager : StateMachine
    {
        #region Const
        private readonly Dictionary<string, int> _typeStateMapper = new Dictionary<string, int>()
    {
        {nameof(SilhouetteBowlInactiveState), 0},
        {nameof(SilhouetteBowlActiveState), 1},
        {nameof(SilhouetteBowlPositiveState), 2},
        {nameof(SilhouetteBowlNegativeState), 3}
    };
        #endregion

        #region SetGet
        public int GetStateNum()
        {
            return _typeStateMapper[CurrentState.GetType().Name];
        }
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            ChangeState<SilhouetteBowlInitState>();
        }
        #endregion
    }
}