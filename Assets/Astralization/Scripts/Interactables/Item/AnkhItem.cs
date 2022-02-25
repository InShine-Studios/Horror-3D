using System;
using UnityEngine;

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
    }
    #endregion

    public override void Use()
    {
        ChangeWorldGM?.Invoke();
        //TODO: Call PlayAudio for Ankh
        //TODO: Implement transition animation
    }
}
