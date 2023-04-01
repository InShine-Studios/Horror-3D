using Astralization.Utils.Helper;
using System;

namespace Astralization.Items.EvidenceItems.ExorcismItems
{
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
            UseBehaviourType = ItemHelper.UseBehaviourType.Handheld;
            WorldConditionType = ItemHelper.WorldConditionType.Real;
        }
        #endregion

        #region Use
        protected override void ActivateFunctionality()
        {
            ExorcismChannelingEvent?.Invoke();
        }
        #endregion
    }
}