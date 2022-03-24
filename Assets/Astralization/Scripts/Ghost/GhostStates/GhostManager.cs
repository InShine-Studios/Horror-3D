using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGhostManager
{
    GhostState GetCurrentGhostState();
}

public class GhostManager : StateMachine, IGhostManager
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

    public GhostState GetCurrentGhostState()
    {
        return _currentGhostState;
    }
    #endregion
}
