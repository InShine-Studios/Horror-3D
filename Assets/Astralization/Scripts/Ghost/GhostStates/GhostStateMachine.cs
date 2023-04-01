using Astralization.SPI;

namespace Astralization.Enemy.EnemyStates
{
    public interface IGhostStateMachine
    {

    }

    public class GhostStateMachine : StateMachine, IGhostStateMachine
    {
        #region MonoBehaviour
        private void Awake()
        {
            ChangeState<GhostInitState>();
        }
        #endregion

        #region Handler
        public void AttemptChasing()
        {
            ChangeState<GhostChasingState>();
            ((GhostChasingState)CurrentState).GhostChasing();
        }
        #endregion
    }
}