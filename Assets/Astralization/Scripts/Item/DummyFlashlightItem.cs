using UnityEngine;

/*
 * DummyFlashlightItem class.
 * Implement mechanics related to flashlight, such as toggle on/off light source.
 * For item testing purpose, will not be implemented in final game.
 */
public class DummyFlashlightItem : Item
{
    #region Variables
    [Header("Light")]
    private Light _lightSource;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _lightSource = GetComponentInChildren<Light>();
        UseBehaviourType = Utils.ItemHelper.UseBehaviourType.None;
        WorldConditionType = Utils.ItemHelper.WorldConditionType.Real;
    }
    #endregion

    #region Use
    public override void Use()
    {
        //Debug.Log("[ITEM] Use " + this.name);
        _lightSource.enabled = !_lightSource.enabled;
        PlayAudio("Flashlight_Switch");
    }
    #endregion
}
