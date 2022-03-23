using UnityEngine;

public class ClockPositiveState : ClockState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        audioInUse = Resources.Load("SFX_Heartbeat_2_Loop", typeof(AudioClip)) as AudioClip;
    }
    #endregion
}
