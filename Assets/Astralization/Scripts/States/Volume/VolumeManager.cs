using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : StateMachine
{
    #region Variables
    [Tooltip("Bool flag to check if the player is in Real World or Astral World")]
    private bool _isInAstralWorld = false;
    #endregion

    #region Getter
    public VolumeState GetCurrentState()
    {
        return (VolumeState)CurrentState;
    }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitVolumeState>();
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
            ChangeState<VolumeAstralState>();
        }
        else
        {
            ChangeState<VolumeRealState>();
        }
    }
    #endregion
}
