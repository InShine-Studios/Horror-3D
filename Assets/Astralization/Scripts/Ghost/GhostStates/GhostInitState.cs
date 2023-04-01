using System.Collections;
using UnityEngine;

namespace Astralization.Enemy.EnemyStates
{
    public class GhostInitState : GhostState
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
            owner.ChangeState<GhostIdleState>();
        }
        #endregion
    }
}