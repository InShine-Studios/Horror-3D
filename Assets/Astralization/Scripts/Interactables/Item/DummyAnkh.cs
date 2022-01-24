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

    private bool _inAstral = false;

    private Color _col;

    #endregion

    public override void Use()
    {
        //Debug.Log("[ITEM] Use " + this.name);
        bool bConverted = ColorUtility.TryParseHtmlString("#5F466A", out _col);
        if (!_inAstral & bConverted) RenderSettings.fogColor = _col;
        else RenderSettings.fogColor = Color.black;
        _astralWorld.SetActive(!_inAstral);
        _inAstral = !_inAstral;
    }
}
