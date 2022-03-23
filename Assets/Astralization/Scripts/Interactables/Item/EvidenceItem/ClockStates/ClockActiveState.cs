using UnityEngine;

public class ClockActiveStates : ClockState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        audioInUse = Resources.Load("SFX_Heartbeat_3_Loop", typeof(AudioClip)) as AudioClip;
    }
    #endregion
}
