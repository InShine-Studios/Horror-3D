using UnityEngine;

/*
 * Class to keep the state of the astral volume settings.
 */
public class VolumeAstral : MonoBehaviour
{
    #region Enable - Disable
    private void OnEnable()
    {
        Ankh.ChangeWorldEvent += ToggleActive;
    }

    private void OnDisable()
    {
        Ankh.ChangeWorldEvent -= ToggleActive;
    }
    #endregion

    private void ToggleActive()
    {
        //Debug.Log("[VOLUME ASTRAL] Toggle " + this.name);

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            go.SetActive(!go.activeInHierarchy);
        }
    }
}
