using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeslotInitState : TimeslotState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        timeNum = -1;
        StartCoroutine(Init());
    }
    #endregion

    #region Initialization
    private IEnumerator Init()
    {
        // set up here for future use
        yield return null;
        owner.ChangeState<TimeslotMorningState>();
    }
    #endregion
}
