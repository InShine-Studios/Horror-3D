using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : StateMachine
{
    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitClockState>();
    }
    #endregion
}
