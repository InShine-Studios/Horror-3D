using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeslotNightState : TimeslotState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        TimeNum = 3;
    }
    #endregion
}
