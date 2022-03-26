using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermometerManager : StateMachine
{
    #region Getter
    public ThermometerState GetCurrentState()
    {
        return (ThermometerState)CurrentState;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitThermometerState>();
    }
    #endregion
}
