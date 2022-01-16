using UnityEngine;

public interface ILightSwitchController: IInteractable
{
    bool GetState();
}

/*
 * Class to control light switch states and sprite.
 * Inherit Interactable.
 */
public class LightSwitchController : Interactable, ILightSwitchController
{
    #region Variables
    [Header("Light switch state")]
    [SerializeField]
    [Tooltip("True if light switch is on")]
    private bool isOn = false;

    [Space]
    [Header("Light")]
    [Tooltip("Light Source")]
    private Light[] lightSources;
    #endregion

    private void Awake()
    {
        lightSources = GetComponentsInChildren<Light>();
    }
    public override void OnInteraction()
    {
        isOn = !isOn;
        SetLightSources(isOn);
        PlayAudio(isOn);
        //Debug.Log(
        //    String.Format("[INTERACTABLE] Turning \"{0}\" {1}", this.name, (isOn ? "on" : "off"))
        //);
    }

    private void SetLightSources(bool value)
    {
        foreach (Light lightSource in lightSources)
        {
            lightSource.enabled = value;
        }
    }

    public bool GetState()
    {
        return isOn;
    }
}
