using UnityEngine;

/*
 * Class to keep the state of the astral volume settings.
 */
public class VolumeAstral : Volume
{
    #region SetGet
    protected override void SetState(bool state)
    {
        //Debug.Log("[WORLD STATE SYSTEM] Toggle " + this.name);
        base.SetState(state);
    }

    protected override void ToggleFogColor(bool state)
    {
        if (state)
        {
            Color col = Utils.ColorHelper.ParseHex("#5F466A");
            RenderSettings.fogColor = col;
        }
    }
    #endregion
}
