using UnityEngine;
using System.Collections.Generic;

public class Clock : EvidenceItem
{

    #region Audio Source
    [Header("Audio Source reference")]
    [SerializeField]
    [Tooltip("Audio Source reference")]
    private GameObject audioSourceReference;
    #endregion

    #region State AudioClips
    [Header("State AudioClips")]
    [SerializeField]
    [Tooltip("AudioClip for Base evidence")]
    private AudioClip baseAudioClip;

    [SerializeField]
    [Tooltip("AudioClip for Detect Evidence evidence")]
    private AudioClip activeAudioClip;

    [SerializeField]
    [Tooltip("AudioClip for Positive evidence")]
    private AudioClip positiveAudioClip;

    [SerializeField]
    [Tooltip("AudioClip for Negative evidence")]
    private AudioClip negativeAudioClip;
    #endregion

    private Dictionary<EvidenceItemState, AudioClip> stateToAudioClipMapping;

    protected override void Awake()
    {
        base.Awake();
        stateToAudioClipMapping = new Dictionary<EvidenceItemState, AudioClip>() {
            {EvidenceItemState.BASE, baseAudioClip},
            {EvidenceItemState.ACTIVE, activeAudioClip},
            {EvidenceItemState.POSITIVE, positiveAudioClip},
            {EvidenceItemState.NEGATIVE, negativeAudioClip},
        };
    }

    private void SetStateAudioClip(AudioClip stateAudioClip) 
    {
        AudioSource audioSource = audioSourceReference.GetComponent<AudioSource>();
        audioSource.clip = stateAudioClip;
    }

    public override void HandleChange()
    {
        SetStateAudioClip(stateToAudioClipMapping[this.state]);
        PlayAudio("StateAudio");
    }

    public override void DetermineEvidence()
    {
        // NOTE this dummy behavior at the moment, wait for Ghost Implementation
        if (state == EvidenceItemState.NEGATIVE) SetState(EvidenceItemState.POSITIVE);
        else SetState(EvidenceItemState.NEGATIVE);
    }
}
