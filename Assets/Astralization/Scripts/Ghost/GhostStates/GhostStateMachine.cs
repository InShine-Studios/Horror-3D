using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGhostStateMachine
{
    GhostState GetCurrentGhostState();
}

public class GhostStateMachine : StateMachine, IGhostStateMachine
{
    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitGhostState>();
    }

    public GhostState GetCurrentGhostState()
    {
        return (GhostState)CurrentState;
    }
    #endregion
}
