using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Volume : MonoBehaviour
{
    public virtual void SetState(bool state)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            go.SetActive(state);
        }
        ToggleFogColor(state);
    }
    public abstract void ToggleFogColor(bool state);
}
