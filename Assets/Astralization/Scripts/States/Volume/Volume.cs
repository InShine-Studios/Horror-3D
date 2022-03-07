using UnityEngine;

/*
 * Abstract Class that is the base of Volumes
 */
public abstract class Volume : MonoBehaviour
{
    #region MonoBehaviour
    private void OnEnable()
    {
        GameManager.ChangeWorldEvent += SetState;
    }

    private void OnDisable()
    {
        GameManager.ChangeWorldEvent -= SetState;
    }

    protected virtual void SetState(bool state)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            go.SetActive(state);
        }
        ToggleFogColor(state);
    }
    protected abstract void ToggleFogColor(bool state);
    #endregion
}
