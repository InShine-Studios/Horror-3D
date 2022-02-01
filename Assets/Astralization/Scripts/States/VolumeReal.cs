using UnityEngine;

/*
 * Class to keep the state of the real world volume settings.
 */
public class VolumeReal : MonoBehaviour
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
        //Debug.Log("[VOLUME REAL] Toggle " + this.name);
        state = !state;
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            go.SetActive(state);
        }
    }
}
