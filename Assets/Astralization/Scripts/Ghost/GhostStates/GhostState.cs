using Astralization.SPI;
using UnityEngine;

namespace Astralization.Enemy.EnemyStates
{
    public abstract class GhostState : State
    {
        #region Variables
        protected GhostStateMachine owner;
        protected Material debugMaterial;
        #endregion

        #region MonoBehaviour
        protected virtual void Awake()
        {
            owner = GetComponent<GhostStateMachine>();
        }
        #endregion

        #region StateHandler
        public override void Enter()
        {
            base.Enter();
            transform.Find("Model").GetComponent<MeshRenderer>().material = debugMaterial;
        }
        #endregion
    }
}