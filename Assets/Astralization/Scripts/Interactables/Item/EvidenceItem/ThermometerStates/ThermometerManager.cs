using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermometerManager : StateMachine
{
    #region Variables
    private ThermometerState _currentThermometerState;
    #endregion

    #region Getter
    public ThermometerState GetCurrentState()
    {
        return _currentThermometerState;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitThermometerState>();
    }
    #endregion

    #region Handler
    public override void ChangeState<T>()
    {
        base.ChangeState<T>();
        _currentThermometerState = (ThermometerState)CurrentState;
    }
    #endregion
}
