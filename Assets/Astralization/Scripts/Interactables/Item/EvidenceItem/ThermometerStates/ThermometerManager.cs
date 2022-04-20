using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThermometerManager : StateMachine
{
    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitThermometerState>();
    }
    #endregion
}
