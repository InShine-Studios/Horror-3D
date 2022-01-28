using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AMDummyFlashlight : AudioManager
{
    #region Enable - Disable
    private void OnEnable()
    {
        DummyFlashlight.PlayAudioEvent += Play;
    }

    private void OnDisable()
    {
        DummyFlashlight.PlayAudioEvent -= Play;
    }
    #endregion
}
