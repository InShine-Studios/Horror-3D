using System.Collections;

namespace Astralization.States.WorldStates
{
    public class WorldInitState : WorldState
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
            owner.ChangeState<WorldRealState>();
        }
        #endregion
    }
}