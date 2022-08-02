using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeslotMorningState : TimeslotState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        timeNum = 0;
        timeName = "Morning";
    }
    #endregion
}
