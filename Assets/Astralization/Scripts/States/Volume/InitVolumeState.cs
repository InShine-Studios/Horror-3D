﻿using System.Collections;
using UnityEngine.InputSystem;

public class InitVolumeState : VolumeState
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
        owner.ChangeState<VolumeRealState>();
    }
    #endregion
}
