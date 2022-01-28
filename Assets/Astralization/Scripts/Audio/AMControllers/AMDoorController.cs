using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMDoorController : AudioManager
{
    #region Enable - Disable
    private void OnEnable()
    {
        DoorController.PlayAudioEvent += Play;
    }

    private void OnDisable()
    {
        DoorController.PlayAudioEvent -= Play;
    }
    #endregion
}
