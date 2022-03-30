using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : StateMachine
{
    #region SetGet
    public ClockState GetCurrentState()
    {
        return (ClockState)CurrentState;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitClockState>();
    }
    #endregion
}
