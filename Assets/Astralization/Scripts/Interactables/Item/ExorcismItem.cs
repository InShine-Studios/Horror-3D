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
    [SerializeField]
    private float _sliderMinValue = 0f;

    private string _playerActionMap = "Exorcism";
    #endregion

    #region Events
    public static event Action<string> ExorcismChannelingEvent;
    public static event Action<float> ExorcismSetMinSliderEvent;
    #endregion

    protected override void Awake()
    {
        base.Awake();
        ExorcismSetMinSliderEvent?.Invoke(_sliderMinValue);
    }
    public override void Use()
    {
        ExorcismChannelingEvent?.Invoke(_playerActionMap);
    }
}
