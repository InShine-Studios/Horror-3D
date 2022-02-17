using UnityEngine;

/*
 * Class to keep the state of the real world volume settings.
 */
public class VolumeReal : Volume
{
    #region Enable - Disable
    private void OnEnable()
    {
        GameManager.ChangeWorldEvent += SetState;
    }

    private void OnDisable()
    {
        GameManager.ChangeWorldEvent -= SetState;
    }
    #endregion

    protected override void SetState(bool state)
    {
        //Debug.Log("[VOLUME REAL] Toggle " + this.name);
        state = !state;
        base.SetState(state);
    }

    protected override void ToggleFogColor(bool state)
    {
        if (state)
        {
            RenderSettings.fogColor = Color.black;
        }
    }
}
