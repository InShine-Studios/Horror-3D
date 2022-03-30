using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostState : State
{
    #region Variables
    protected GhostStateMachine owner;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<GhostStateMachine>();
    }
    #endregion
}
