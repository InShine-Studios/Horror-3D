using System;
using UnityEngine;

/*
 * Ankh class.
 * Implement mechanics related to ankh item, such as toggle on/off astral world.
 */
public class Ankh : Item
{
    #region Variables
    private bool _inAstral = false; //TODO: Refactor this to GM

    public static event Action ChangeWorldEvent; //TODO: Refactor this to GM

    #endregion

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Use()
    {
        //Debug.Log("[ITEM] Use " + this.name);
        ChangeWorldEvent?.Invoke();

        _inAstral = !_inAstral;
        if (_inAstral)
        {
            Color col = Utils.ColorHelper.ParseHex("#5F466A");
            RenderSettings.fogColor = col;
        }
        else RenderSettings.fogColor = Color.black;

        //TODO: Call PlayAudio for Ankh
        //TODO: Implement transition animation
    }
}
