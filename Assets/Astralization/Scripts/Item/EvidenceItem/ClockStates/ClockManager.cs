using Astralization.SPI;
using System.Collections.Generic;

namespace Astralization.Items.EvidenceItem.ClockStates
{
    public class ClockManager : StateMachine
    {
        #region Const
        private readonly Dictionary<string, int> _typeStateMapper = new Dictionary<string, int>()
    {
        {nameof(ClockInactiveState), 0},
        {nameof(ClockActiveState), 1},
        {nameof(ClockPositiveState), 2},
        {nameof(ClockNegativeState), 3}
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
            ChangeState<ClockInitState>();
        }
        #endregion
    }
}