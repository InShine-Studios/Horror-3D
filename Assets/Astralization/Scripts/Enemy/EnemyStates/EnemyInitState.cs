using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInitState : EnemyState
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
        owner.ChangeState<EnemyIdleState>();
    }
    #endregion
}
