using System;

public interface IExorcismItem
{

}

/*
 * Class to use Exorcism Item.
 * HUD managed by ExorcismBar.
 */
public class ExorcismItem : Item, IExorcismItem
{
    #region Events
    public static event Action ExorcismChannelingEvent;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        UseBehaviourType = Utils.ItemHelper.UseBehaviourType.Handheld;
        WorldConditionType = Utils.ItemHelper.WorldConditionType.Real;
    }
    #endregion

    #region Use
    protected override void ActivateFunctionality()
    {
        ExorcismChannelingEvent?.Invoke();
    }
    #endregion
}
