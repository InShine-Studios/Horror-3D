using UnityEngine;

public class ClockState : State
{
    #region Variables
    private GameObject _audioSourceReference;
    private AudioSource _audioSource;
    protected ClockManager owner;
    protected AudioClip audioInUse;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<ClockManager>();
        _audioSourceReference = transform.Find("AudioPlayer/StateAudio").gameObject;
        _audioSource = _audioSourceReference.GetComponent<AudioSource>();
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
