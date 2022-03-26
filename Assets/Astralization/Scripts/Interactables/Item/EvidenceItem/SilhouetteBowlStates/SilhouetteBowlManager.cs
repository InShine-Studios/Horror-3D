using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteBowlManager : StateMachine
{
    #region SetGet
    public SilhouetteBowlState GetCurrentState()
    {
        return (SilhouetteBowlState)CurrentState;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitSilhouetteBowlState>();
    }
    #endregion
}
