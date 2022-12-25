using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCluesMindMapState : PlayerState
{
    #region Events
    #endregion

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
    }

    public override void Exit()
    {
        base.Exit();
        Cursor.lockState = CursorLockMode.Locked;
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
