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

    #endregion

    public override void Use()
    {
        //Debug.Log("[ITEM] Use " + this.name);
        _inAstral = !_inAstral;
        Color col = Utils.ColorParser.GetColor("#ZZZZZZ");
        if (_inAstral) RenderSettings.fogColor = col;
        else RenderSettings.fogColor = Color.black;
        _astralWorld.SetActive(_inAstral);
    }
}
