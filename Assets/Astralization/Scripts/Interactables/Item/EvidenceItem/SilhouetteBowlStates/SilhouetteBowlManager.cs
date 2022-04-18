using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilhouetteBowlManager : StateMachine
{
    #region MonoBehaviour
    private void Awake()
    {
        ChangeState<InitSilhouetteBowlState>();
    }
    #endregion
}
