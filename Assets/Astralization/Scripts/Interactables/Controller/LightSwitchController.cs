using System;
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
    private bool _isOn = false;

    [Space]
    [Header("Light")]
    [Tooltip("Light Source")]
    private Light[] _lightSources;
    #endregion

    public static event Action<string> PlayAudioEvent;

    private void Awake()
    {
        _lightSources = GetComponentsInChildren<Light>();
    }
    public override void OnInteraction()
    {
        //Debug.Log(
        //    String.Format("[INTERACTABLE] Turning \"{0}\" {1}", this.name, (isOn ? "on" : "off"))
        //);
        _isOn = !_isOn;
        SetLightSources(_isOn);
        if(_isOn) PlayAudioEvent?.Invoke("Switch_On");
        else PlayAudioEvent?.Invoke("Switch_Off");
    }

    private void SetLightSources(bool value)
    {
        foreach (Light lightSource in _lightSources)
        {
            lightSource.enabled = value;
        }
    }

    public bool GetState()
    {
        return _isOn;
    }
}
