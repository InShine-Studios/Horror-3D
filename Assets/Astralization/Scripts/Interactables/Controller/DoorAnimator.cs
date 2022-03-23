using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    #region Variables
    private DoorController _doorController;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _doorController = GetComponentInChildren<DoorController>();
    }
    #endregion

    #region Handler
    public void StartTransition()
    {
        _doorController.SetEnableChangeState(false);
    }

    public void FinishTransition()
    {
        _doorController.SetEnableChangeState(true);
    }
    #endregion
}
