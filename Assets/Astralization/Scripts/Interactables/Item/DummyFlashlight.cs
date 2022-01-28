using System;
using UnityEngine;

/*
 * DummyFlashlight class.
 * Implement mechanics related to flashlight, such as toggle on/off light source.
 * For item testing purpose, will not be implemented in final game.
 */
public class DummyFlashlight : Item
{
    #region Variables
    [Header("Light")]
    private Light _lightSource;

    public static event Action<string> PlayAudioEvent;

    #endregion

    private void Awake()
    {
        _lightSource = GetComponentInChildren<Light>();
    }

    public override void Use()
    {
        //Debug.Log("[ITEM] Use " + this.name);
        _lightSource.enabled = !_lightSource.enabled;
        //PlayAudio("Flashlight_Switch");
        PlayAudioEvent?.Invoke("Flashlight_Switch");
    }
}
