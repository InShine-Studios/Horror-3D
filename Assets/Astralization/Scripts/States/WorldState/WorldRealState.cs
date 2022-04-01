using UnityEngine;

public interface IWorldRealState { }

public class WorldRealState : WorldState, IWorldRealState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        colorInUse = Color.black;
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
        astralMeterLogic.SetRealRate();
        volumeReal.SetActive(true);
        volumeAstral.SetActive(false);
    }
    #endregion
}
