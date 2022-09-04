using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeslotAfternoonState : TimeslotState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        timeNum = 1;
        timeName = "Afternoon";
    }
    #endregion
}