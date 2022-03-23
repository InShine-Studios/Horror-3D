using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteBowlManager : StateMachine
{
    #region Variables
    private SilhouetteBowlState _currentSilhouetteBowlState;
    #endregion

    #region Getter
    public SilhouetteBowlState GetCurrentState()
    {
        return _currentSilhouetteBowlState;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitSilhouetteBowlState>();
    }
    #endregion

    #region Handler
    public override void ChangeState<T>()
    {
        base.ChangeState<T>();
        _currentSilhouetteBowlState = (SilhouetteBowlState)CurrentState;
    }
    #endregion
}
