using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockInactiveState : ClockState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        audioInUse = null;
    }
    #endregion
}
