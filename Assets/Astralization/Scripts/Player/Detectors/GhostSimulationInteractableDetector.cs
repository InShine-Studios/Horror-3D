using Astralization.Items;
using Astralization.Items.EvidenceItems;
using UnityEngine;

namespace Astralization.Player.Detectors
{

    /*
     * Subclass of ObjectDetector class.
     * Used to temporary simulate Ghost "Interact" action.
     * 
     * Will call the onGhostInteraction function on the Interactable object.
     */
    [RequireComponent(typeof(CapsuleCollider))]
    public class GhostSimulationInteractableDetector : ItemDetector
    {
        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            detectionTag = "Item";
        }
        #endregion

        #region Handler
        protected override void InteractClosest(Item closest)
        {
            //Debug.Log("[PLAYER INTERACTION] Interacted with " + closest.name);
            if (closest is EvidenceItem)
            {
                EvidenceItem castedClosest = (EvidenceItem)closest;
                castedClosest.OnGhostInteraction();
            }
        }
        #endregion
    }
}