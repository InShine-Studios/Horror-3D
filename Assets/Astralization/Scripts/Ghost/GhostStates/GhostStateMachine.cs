using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGhostStateMachine
{

}

public class GhostStateMachine : StateMachine, IGhostStateMachine
{
    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<GhostInitState>();
    }
    #endregion
}
