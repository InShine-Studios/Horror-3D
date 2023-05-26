using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : State
{
    #region Variables
    protected EnemyStateMachine owner;
    protected Material debugMaterial;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<EnemyStateMachine>();
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
