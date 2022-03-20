using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GhostState : State
{
    #region Variables
    protected GhostManager owner;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<GhostManager>();
    }
    #endregion

    #region Handler
    public virtual void ChangeToWanderInSeconds(float delay)
    {
        Utils.LoggerHelper.PrintStateDefaultLog("Ghost", "ChangeToWanderInSeconds");
    }
    #endregion
}
