using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockManager : StateMachine
{
    #region Variables
    private ClockState _currentClockState;
    #endregion

    #region Getter
    public ClockState GetCurrentState()
    {
        return _currentClockState;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitClockState>();
    }
    #endregion

    #region Handler
    public override void ChangeState<T>()
    {
        base.ChangeState<T>();
        _currentClockState = (ClockState)CurrentState;
    }
    #endregion
}
