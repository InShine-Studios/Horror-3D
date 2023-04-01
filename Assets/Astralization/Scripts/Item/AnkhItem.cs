using Astralization.Utils.Calculation;
using Astralization.Utils.Helper;
using System;

namespace Astralization.Items
{
    internal enum AnkhState : int
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
            UseBehaviourType = ItemHelper.UseBehaviourType.Handheld;
            WorldConditionType = ItemHelper.WorldConditionType.Real | ItemHelper.WorldConditionType.Astral;
            LogoState = (int)AnkhState.Inactive;
            base.Awake();
        }
        #endregion

        #region Use
        protected override void ActivateFunctionality()
        {
            ChangeWorldGM?.Invoke();
            LogoState = MathCalcu.mod(LogoState + 1, 2);
            //TODO: Call PlayAudio for Ankh
            //TODO: Implement transition animation
        }
        #endregion
    }
}