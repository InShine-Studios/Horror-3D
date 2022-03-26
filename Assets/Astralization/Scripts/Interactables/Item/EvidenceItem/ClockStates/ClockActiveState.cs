using UnityEngine;

public class ClockActiveState : ClockState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        audioInUse = Resources.Load("EvidenceItem/SFX_Clock_3_Loop", typeof(AudioClip)) as AudioClip;
    }
    #endregion
}
