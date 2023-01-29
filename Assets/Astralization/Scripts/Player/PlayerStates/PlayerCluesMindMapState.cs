using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// TODO: implement pause menu control here, rename to PlayerPauseState
public class PlayerCluesMindMapState : PlayerState
{
    #region Variables
    private MindMapCameraManager _mindMapCameraManager;
    private MindMapTree _mindMapTree;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _mindMapCameraManager = this.transform.parent.GetComponentInChildren<MindMapCameraManager>();
        _mindMapTree = this.transform.parent.GetComponentInChildren<MindMapTree>();
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
        Cursor.lockState = CursorLockMode.Confined;
        owner.InvokeOpenMindMap();
    }

    public override void Exit()
    {
        base.Exit();
        Cursor.lockState = CursorLockMode.Locked;
        owner.InvokeCloseMindMap();
    }
    #endregion

    #region InputHandler
        public override void ChangeCore(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _mindMapTree.ChangeCore((int)ctx.ReadValue<float>());
        }
    }

    public override void ChangeClue(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            _mindMapTree.ChangeClue((int)ctx.ReadValue<float>());
        }
    }
    #endregion
}
