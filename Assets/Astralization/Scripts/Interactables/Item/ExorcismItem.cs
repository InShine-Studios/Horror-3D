using System;

public interface IExorcismItem
{
    void Use();
}

/*
 * Class to use Exorcism Item.
 * HUD managed by ExorcismBar.
 */
public class ExorcismItem : Item, IExorcismItem
{
    #region Events
    public static event Action<string> ExorcismChannelingEvent;
    #endregion

    #region Variables
    private string _playerActionMap = "Exorcism";
    #endregion

    #region Use
    public override void Use()
    {
        ExorcismChannelingEvent?.Invoke(_playerActionMap);
    }
    #endregion
}
