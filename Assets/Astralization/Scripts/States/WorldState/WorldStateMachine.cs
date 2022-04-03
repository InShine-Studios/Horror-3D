using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStateMachine : StateMachine
{
    #region Variables
    [Tooltip("Bool flag to check if the player is in Real World or Astral World")]
    private bool _isInAstralWorld = false;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<WorldInitState>();
    }

    private void OnEnable()
    {
        GameManager.ChangeWorldEvent += ChangeState;
    }

    private void OnDisable()
    {
        GameManager.ChangeWorldEvent -= ChangeState;
    }
    #endregion

    #region WorldHandler
    protected virtual void ChangeState()
    {
        _isInAstralWorld = !_isInAstralWorld;
        if (_isInAstralWorld)
        {
            ChangeState<WorldAstralState>();
            //Debug.Log("[WORLD STATE] Changing world state to Astral");
        }
        else
        {
            ChangeState<WorldRealState>();
            //Debug.Log("[WORLD STATE] Changing world state to Real");
        }
    }
    #endregion
}
