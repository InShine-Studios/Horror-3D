using UnityEngine;

public interface IWorldState { }

public class WorldState : State, IWorldState
{
    #region Variables
    protected WorldStateMachine owner;
    protected Color colorInUse;
    protected GameObject volumeAstral;
    protected GameObject volumeReal;
    protected AstralMeterLogic astralMeterLogic;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<WorldStateMachine>();
        astralMeterLogic = GetComponent<AstralMeterLogic>();
        volumeAstral = transform.Find("VOL_AstralWorld").gameObject;
        volumeReal = transform.Find("VOL_RealWorld").gameObject;
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
        RenderSettings.fogColor = colorInUse;
    }
    #endregion
}
