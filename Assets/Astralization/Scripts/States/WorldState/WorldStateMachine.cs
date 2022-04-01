using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateMachine : StateMachine
{
    #region Variables
    [Tooltip("Bool flag to check if the player is in Real World or Astral World")]
    private bool _isInAstralWorld = false;
    #endregion

    #region Getter
    public WorldState GetCurrentState()
    {
        return (WorldState)CurrentState;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitWorldState>();
    }

    private void OnEnable()
    {
        GameManager.ChangeWorldEvent += SetState;
    }

    private void OnDisable()
    {
        GameManager.ChangeWorldEvent -= SetState;
    }

    protected virtual void SetState()
    {
        _isInAstralWorld = !_isInAstralWorld;
        if (_isInAstralWorld)
        {
            ChangeState<WorldAstralState>();
            //Debug.Log("[MANAGER] Changing world state to Astral");
        }
        else
        {
            ChangeState<WorldRealState>();
            //Debug.Log("[MANAGER] Changing world state to Real");
        }
    }
    #endregion
}
