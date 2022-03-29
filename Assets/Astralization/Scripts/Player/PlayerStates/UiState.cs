using UnityEngine;
using Cinemachine;
using System;

/*
 * Class to manage player when in UI state (dialogue, journal, etc).
 * All logic-related players in UI state will be handled here.
 */
public class UiState : PlayerState
{
    #region Variables
    private CinemachineBrain _cinemachineBrain;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _cinemachineBrain = GetComponent<PlayerMovement>().GetMainCamera().GetComponent<CinemachineBrain>();
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
        Cursor.lockState = CursorLockMode.Confined;
        StartCoroutine(Utils.DelayerHelper.Delay(0.25f, () => _cinemachineBrain.enabled = false));
    }

    public override void Exit()
    {
        base.Exit();
        Cursor.lockState = CursorLockMode.Locked;
        _cinemachineBrain.enabled = true;
    }
    #endregion
}
