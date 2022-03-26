using UnityEngine;

public class ClockPositiveState : ClockState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        audioInUse = Resources.Load("EvidenceItem/SFX_Clock_2_Loop", typeof(AudioClip)) as AudioClip;
    }
    #endregion
}
