using UnityEngine;

public interface IWorldAstralState { }

public class WorldAstralState : WorldState, IWorldAstralState
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        colorInUse = Utils.ColorHelper.ParseHex("#5F466A");
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
        astralMeterLogic.SetAstralRate();
        volumeReal.SetActive(false);
        volumeAstral.SetActive(true);
    }
    #endregion
}
