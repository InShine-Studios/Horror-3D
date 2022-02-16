using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Volume : MonoBehaviour
{
    public abstract void SetState(bool state);
    public abstract void ToggleFogColor(bool state);
}
