using UnityEngine;

public interface IVolumeAstralState { }

public class VolumeAstralState : VolumeState, IVolumeAstralState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        colorInUse = Utils.ColorHelper.ParseHex("#5F466A");
    }
    #endregion

    #region Handler
    public override void Enter()
    {
        base.Enter();
        volumeReal.SetActive(false);
        volumeAstral.SetActive(true);
    }
    #endregion
}
