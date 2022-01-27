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
        if (_inAstral)
        {
            Color col = Utils.ColorHelper.ParseHex("#5F466A");
            RenderSettings.fogColor = col;
        }
        else RenderSettings.fogColor = Color.black;
        _astralWorld.SetActive(_inAstral);
        //TODO: Call PlayAudio for Ankh
        //TODO: Implement transition animation
    }
}
