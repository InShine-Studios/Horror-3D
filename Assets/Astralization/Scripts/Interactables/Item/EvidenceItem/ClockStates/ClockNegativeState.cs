using UnityEngine;

public class ClockNegativeState : ClockState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        audioInUse = Resources.Load("SFX_Heartbeat_1_Loop", typeof(AudioClip)) as AudioClip;
    }
    #endregion
}
