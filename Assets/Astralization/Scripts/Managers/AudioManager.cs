using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The class for managing audio, specifically audio that needs to play globally i.e. ambience etc.
 */
public class AudioManager : MonoBehaviour
{
    [Space]
    [Header("Audio")]
    [Tooltip("Audio Manager")]
    private AudioPlayer _audioPlayerObj;

    protected virtual void Awake()
    {
        _audioPlayerObj = GetComponentInChildren<AudioPlayer>();
    }

    #region Enable - Disable
    private void OnEnable()
    {
        GameManager.PlayerAudioDiesEvent += PlayAudioDies;
    }

    private void OnDisable()
    {
        GameManager.PlayerAudioDiesEvent -= PlayAudioDies;
    }
    #endregion

    private void PlayAudioDies()
    {
        _audioPlayerObj.Play("FemaleScream");
    }
}
