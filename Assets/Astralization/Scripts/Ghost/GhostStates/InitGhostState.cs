using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitGhostState : GhostState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        StartCoroutine(Init());
    }
    #endregion

    #region Handler
    private IEnumerator Init()
    {
        // set up here for future use
        yield return null;
        owner.ChangeState<IdleGhostState>();
    }
    #endregion
}
