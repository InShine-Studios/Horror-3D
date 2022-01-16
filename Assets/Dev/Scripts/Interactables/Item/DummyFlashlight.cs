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
    private Light lightSource;

    #endregion

    private void Awake()
    {
        lightSource = GetComponentInChildren<Light>();
    }

    public override void Use()
    {
        //Debug.Log("[ITEM] Use " + this.name);
        lightSource.enabled = !lightSource.enabled;
        PlayAudio("Flashlight_Switch");
    }
}
