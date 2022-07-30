using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeslotMorningState : TimeslotState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        TimeNum = 0;
    }
    #endregion
}
