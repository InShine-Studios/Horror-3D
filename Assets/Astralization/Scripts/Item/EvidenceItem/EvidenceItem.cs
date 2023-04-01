using Astralization.Utils.Helper;

namespace Astralization.Items.EvidenceItems
{
    public interface IEvidenceItem : IItem
    {
        void DetermineEvidence();
        void OnGhostInteraction();
    }

    /*
     * EvidenceItem abstract class.
     * Parent class for all evidence item objects.
     * Implement DetermineEvidence() and HandleChange() function on each child class.
     */
    public abstract class EvidenceItem : Item, IEvidenceItem
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            UseBehaviourType = ItemHelper.UseBehaviourType.Environmental;
            WorldConditionType = ItemHelper.WorldConditionType.Real | ItemHelper.WorldConditionType.Astral;
        }
        #endregion

        #region GhostInput
        public virtual void OnGhostInteraction()
        {
            UpdateMarker();
        }

        #endregion

        #region EvidenceHelper
        public abstract void DetermineEvidence();
        #endregion
    }
}