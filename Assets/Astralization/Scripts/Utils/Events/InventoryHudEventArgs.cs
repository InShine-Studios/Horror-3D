using System;
using UnityEngine;

namespace Astralization.Utils.Events
{

    #region EventArgs
    public abstract class InventoryHudEventArgs : EventArgs
    {
        public int InventoryLength;
        public int CurrentActiveIdx;
        public int LogoAnimatorIdx;
        public RuntimeAnimatorController HudLogoAnimatorController;
        public int HudLogoAnimationParam;
    }

    public class InitInventoryHudEventArgs : InventoryHudEventArgs
    {
        public InitInventoryHudEventArgs(int inventoryLenght, int currentActiveIdx)
        {
            InventoryLength = inventoryLenght;
            CurrentActiveIdx = currentActiveIdx;
        }
    }

    public class UpdateHudLogoEventArgs : InventoryHudEventArgs
    {
        public UpdateHudLogoEventArgs(int logoAnimatorIdx, RuntimeAnimatorController hudLogoAnimatorController, int hudLogoAnimationParam = -1)
        {
            LogoAnimatorIdx = logoAnimatorIdx;
            HudLogoAnimatorController = hudLogoAnimatorController;
            HudLogoAnimationParam = hudLogoAnimationParam;
        }
    }

    public class ChangeActiveItemIdxEventArgs : InventoryHudEventArgs
    {
        public ChangeActiveItemIdxEventArgs(int currentActiveIdx)
        {
            CurrentActiveIdx = currentActiveIdx;
        }
    }

    public class ChangeActiveItemAnimEventArgs : InventoryHudEventArgs
    {
        public ChangeActiveItemAnimEventArgs(int hudLogoAnimationParam)
        {
            HudLogoAnimationParam = hudLogoAnimationParam;
        }
    }

    [Obsolete("Method is obsolete.", false)]
    public class ToggleExpandShrinkEventArgs : InventoryHudEventArgs
    {
        public ToggleExpandShrinkEventArgs() { }
    }
}
#endregion
