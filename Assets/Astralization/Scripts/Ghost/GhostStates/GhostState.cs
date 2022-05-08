using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
