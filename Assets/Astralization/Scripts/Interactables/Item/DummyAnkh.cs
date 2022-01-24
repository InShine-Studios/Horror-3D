using UnityEngine;


/*
 * DummyFlashlight class.
 * Implement mechanics related to flashlight, such as toggle on/off light source.
 * For item testing purpose, will not be implemented in final game.
 */
public class DummyAnkh : Item
{
    #region Variables
    [Tooltip("Astral World")]
    [SerializeField]
    private GameObject _astralWorld;

    #endregion

    public override void Use()
    {
        //Debug.Log("[ITEM] Use " + this.name);
        _astralWorld.SetActive(!_astralWorld.activeInHierarchy);
    }
}
