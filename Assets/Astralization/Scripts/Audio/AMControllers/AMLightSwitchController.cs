using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMLightSwitchController : AudioManager
{
    #region Enable - Disable
    private void OnEnable()
    {
        LightSwitchController.PlayAudioEvent += Play;
    }

    private void OnDisable()
    {
        LightSwitchController.PlayAudioEvent -= Play;
    }
    #endregion
}
