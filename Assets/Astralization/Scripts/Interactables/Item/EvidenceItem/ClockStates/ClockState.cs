using UnityEngine;

public class ClockState : State
{
    #region Variables
    private GameObject _audioSourceReference;
    protected ClockManager owner;
    protected AudioClip audioInUse;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<ClockManager>();
        _audioSourceReference = transform.Find("AudioPlayer/StateAudio").gameObject;
    }
    #endregion

    #region Handler
    public override void Enter()
    {
        base.Enter();
        AudioSource audioSource = _audioSourceReference.GetComponent<AudioSource>();
        audioSource.clip = audioInUse;
    }
    #endregion
}
