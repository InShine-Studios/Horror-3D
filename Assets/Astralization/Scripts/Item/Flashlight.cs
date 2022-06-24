using UnityEngine;

public interface IFlashlight
{
    bool IsOn { get; }

    void ToggleOnOff();
}

/*
 * Flashlight class.
 * Implement mechanics related to flashlight, such as toggle on/off light source.
 */
public class Flashlight : MonoBehaviour, IFlashlight
{
    #region Variables
    [Header("Light")]
    private Light _lightSource;

    public bool IsOn { get; private set; }

    [Space]
    [Header("Audio")]
    [Tooltip("Audio Manager")]
    private AudioPlayer _audioPlayerObj;
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        _lightSource = GetComponentInChildren<Light>();
        _audioPlayerObj = GetComponentInChildren<AudioPlayer>();
        IsOn = _lightSource.enabled;
    }
    #endregion

    #region Functionality
    public void ToggleOnOff()
    {
        _lightSource.enabled = !_lightSource.enabled;
        IsOn = _lightSource.enabled;
        PlayAudio("Flashlight_Switch");
    }
    #endregion

    #region AudioHandler

    private void PlayAudio(string name)
    {
        _audioPlayerObj.Play(name);
    }
    #endregion
}
