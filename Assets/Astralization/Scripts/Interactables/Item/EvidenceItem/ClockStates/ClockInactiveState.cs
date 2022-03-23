using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockInactiveStates : ClockState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        audioInUse = null;
    }
    #endregion
}
