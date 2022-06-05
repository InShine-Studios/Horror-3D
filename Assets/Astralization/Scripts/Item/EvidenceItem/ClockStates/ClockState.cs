using UnityEngine;

public class ClockState : State, IEvidenceState
{
    #region Variables
    private AudioSource _audioSource;
    protected ClockManager owner;
    protected AudioClip audioInUse;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<ClockManager>();
        GameObject StateAudio = transform.Find("AudioPlayer/StateAudio").gameObject;
        _audioSource = StateAudio.GetComponent<AudioSource>();
    }
    #endregion

    #region Handler
    public override void Enter()
    {
        base.Enter();
        _audioSource.clip = audioInUse;
    }
    #endregion
}
