using System;
using UnityEngine;
using UnityEngine.InputSystem;

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
    #region Variables
    private string _playerActionMap = "Exorcism";
    #endregion

    #region Events
    public static event Action<string> ExorcismChannelingEvent;
    #endregion

    public override void Use()
    {
        ExorcismChannelingEvent?.Invoke(_playerActionMap);
    }
}
