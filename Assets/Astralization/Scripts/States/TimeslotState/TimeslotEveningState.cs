using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeslotEveningState : TimeslotState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        timeNum = 2;
        timeName = "Evening";
    }
    #endregion
}
