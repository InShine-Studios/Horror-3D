using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGhostManager
{
    GhostState GetCurrentGhostState();
}

public class GhostStateMachine : StateMachine, IGhostManager
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
