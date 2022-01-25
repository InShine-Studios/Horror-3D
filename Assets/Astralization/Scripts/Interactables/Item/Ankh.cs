using UnityEngine;
/*
 * Ankh class.
 * Implement mechanics related to ankh item, such as toggle on/off astral world.
 */
public class Ankh : Item
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
        _inAstral = !_inAstral;
        Color _col = Utils.ColorParser.GetColor("#5F466A");
        if (_inAstral) RenderSettings.fogColor = _col;
        else RenderSettings.fogColor = Color.black;
        _astralWorld.SetActive(_inAstral);
    }
}
