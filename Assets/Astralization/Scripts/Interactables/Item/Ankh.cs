using System;
using UnityEngine;

/*
 * Ankh class.
 * Implement mechanics related to ankh item, such as toggle on/off astral world.
 */
public class Ankh : Item
{
    #region Variables
    public static event Action ChangeWorldGM;

    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Use()
    {
        ChangeWorldGM?.Invoke();
        //TODO: Call PlayAudio for Ankh
        //TODO: Implement transition animation
    }
}
