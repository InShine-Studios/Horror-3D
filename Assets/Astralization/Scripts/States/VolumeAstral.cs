using UnityEngine;

/*
 * Class to keep the state of the astral volume settings.
 */
public class VolumeAstral : MonoBehaviour
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

    private void SetState(bool state)
    {
        //Debug.Log("[VOLUME ASTRAL] Toggle " + this.name);

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            go.SetActive(state);
        }
        ToggleFogColor(state);
    }

    private void ToggleFogColor(bool state)
    {
        if (state)
        {
            Color col = Utils.ColorHelper.ParseHex("#5F466A");
            RenderSettings.fogColor = col;
        }
        else RenderSettings.fogColor = Color.black;
    }
}
