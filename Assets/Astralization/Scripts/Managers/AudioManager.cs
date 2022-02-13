using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Space]
    [Header("Audio")]
    [Tooltip("Audio Manager")]
    private AudioPlayer AudioPlayerObj;

    protected virtual void Awake()
    {
        AudioPlayerObj = GetComponentInChildren<AudioPlayer>();
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
        AudioPlayerObj.Play("FemaleScream");
    }
}
