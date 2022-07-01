using UnityEngine;

public interface IWorldState { }

public class WorldState : State, IWorldState
{
    #region Variables
    protected WorldStateMachine owner;
    protected Color colorInUse;
    protected AstralMeterLogic astralMeterLogic;
    protected GameObject volumeInUse;
    protected Utils.ItemHelper.WorldConditionType worldCondition;
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        owner = GetComponent<WorldStateMachine>();
        astralMeterLogic = AstralMeterLogic.Instance;
    }
    #endregion

    #region StateHandler
    public override void Enter()
    {
        base.Enter();
        RenderSettings.fogColor = colorInUse;
        if(volumeInUse != null)
        {
            volumeInUse.SetActive(true);
        }
    }

    public override void Exit()
    {
        base.Exit();
        if (volumeInUse != null)
        {
            volumeInUse.SetActive(false);
        }
    }
    #endregion
}
