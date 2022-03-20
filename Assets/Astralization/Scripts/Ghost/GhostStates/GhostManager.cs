using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostManager : StateMachine
{
    #region Variables
    private GhostState _currentGhostState;
    #endregion

    #region SetGet
    public override void ChangeState<T>()
    {
        base.ChangeState<T>();
        _currentGhostState = (GhostState)CurrentState;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitGhostState>();
    }
    #endregion

    #region Handler

    #endregion
}
