using UnityEngine;

/*
 * The class for managing audio, specifically audio that needs to play globally i.e. ambience etc.
 */
public class AudioManager : MonoBehaviour
{
    #region Variables
    [Header("Audio")]
    [Tooltip("Audio Manager")]
    private AudioPlayer _audioPlayerObj;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        _audioPlayerObj = GetComponentInChildren<AudioPlayer>();
    }

    private void OnEnable()
    {
        GameManager.PlayerAudioDiesEvent += PlayAudioDies;
    }

    private void OnDisable()
    {
        GameManager.PlayerAudioDiesEvent -= PlayAudioDies;
    }
    #endregion

    #region Handler
    private void PlayAudioDies()
    {
        _audioPlayerObj.Play("FemaleScream");
    }
    #endregion
}
