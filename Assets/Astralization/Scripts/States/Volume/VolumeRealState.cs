using UnityEngine;

public interface IVolumeRealState { }

public class VolumeRealState : VolumeState, IVolumeRealState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        colorInUse = Color.black;
    }
    #endregion

    #region Handler
    public override void Enter()
    {
        base.Enter();
        volumeReal.SetActive(true);
        volumeAstral.SetActive(false);
    }
    #endregion
}
