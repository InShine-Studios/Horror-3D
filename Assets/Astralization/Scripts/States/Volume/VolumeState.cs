using UnityEngine;

public interface IVolumeState { }

public class VolumeState : State, IVolumeState
{
    #region Variables
    protected VolumeManager owner;
    protected Color colorInUse;
    protected GameObject volumeAstral;
    protected GameObject volumeReal;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<VolumeManager>();
        volumeAstral = transform.Find("VOL_AstralWorld").gameObject;
        volumeReal = transform.Find("VOL_RealWorld").gameObject;
    }
    #endregion

    #region Handler
    public override void Enter()
    {
        base.Enter();
        RenderSettings.fogColor = colorInUse;
    }
    #endregion
}