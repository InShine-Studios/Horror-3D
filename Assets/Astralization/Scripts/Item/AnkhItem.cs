using System;
using System.Collections.Generic;
using UnityEngine;

enum AnkhState : int
{
    Inactive = 0,
    Active = 1
}

/*
 * Ankh class.
 * Implement mechanics related to ankh item, such as toggle on/off astral world.
 */
public class AnkhItem : Item
{
    #region Events
    public static event Action ChangeWorldGM;
    #endregion

    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        UseBehaviourType = Utils.ItemHelper.UseBehaviourType.Handheld;
        WorldConditionType = Utils.ItemHelper.WorldConditionType.Real | Utils.ItemHelper.WorldConditionType.Astral;
        LogoState = (int)AnkhState.Inactive;
    }
    #endregion

    #region Use
    protected override void ActivateFunctionality()
    {
        ChangeWorldGM?.Invoke();
        LogoState = Utils.MathCalcu.mod(LogoState + 1, 2);
        //TODO: Call PlayAudio for Ankh
        //TODO: Implement transition animation
    }
    #endregion
}
