using UnityEngine;
using System.Collections.Generic;

/*
 * Clock class.
 * Override DetermineEvidence and HandleChange from base EvidenceItem class according to murder time stamp evidence mechanics.
 */
public class ClockItem : EvidenceItem
{
    #region Variables - AudioStates
    [Header("Audio Source reference")]
    [SerializeField]
    [Tooltip("Audio Source reference")]
    private GameObject _audioSourceReference;

    [Header("State AudioClips")]
    [SerializeField]
    [Tooltip("AudioClip for Base evidence")]
    private AudioClip _baseAudioClip;

    [Space]
    [SerializeField]
    [Tooltip("AudioClip for Detect Evidence evidence")]
    private AudioClip _activeAudioClip;

    [Space]
    [SerializeField]
    [Tooltip("AudioClip for Positive evidence")]
    private AudioClip _positiveAudioClip;

    [Space]
    [SerializeField]
    [Tooltip("AudioClip for Negative evidence")]
    private AudioClip _negativeAudioClip;
    #endregion

    #region Variables
    private Dictionary<EvidenceItemState, AudioClip> _stateToAudioClipMapping;
    #endregion

    #region SetGet
    private void SetStateAudioClip(AudioClip stateAudioClip)
    {
        AudioSource audioSource = _audioSourceReference.GetComponent<AudioSource>();
        audioSource.clip = stateAudioClip;
    }
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        _stateToAudioClipMapping = new Dictionary<EvidenceItemState, AudioClip>() {
            {EvidenceItemState.BASE, _baseAudioClip},
            {EvidenceItemState.ACTIVE, _activeAudioClip},
            {EvidenceItemState.POSITIVE, _positiveAudioClip},
            {EvidenceItemState.NEGATIVE, _negativeAudioClip},
        };
    }
    #endregion

    #region Handler
    public override void HandleChange()
    {
        SetStateAudioClip(_stateToAudioClipMapping[this.state]);
        PlayAudio("StateAudio");
    }
    #endregion

    #region Evidence Related
    public override void DetermineEvidence()
    {
        // TODO this dummy behavior at the moment, wait for Ghost Implementation
        if (state == EvidenceItemState.NEGATIVE) SetState(EvidenceItemState.POSITIVE);
        else SetState(EvidenceItemState.NEGATIVE);
    }
    #endregion
}
