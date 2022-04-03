using UnityEngine;

public interface IWorldRealState { }

public class WorldRealState : WorldState, IWorldRealState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        colorInUse = Color.black;
        volumeInUse = transform.Find("VOL_RealWorld").gameObject;
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
        astralMeterLogic.SetRealRate();
    }
    #endregion
}
