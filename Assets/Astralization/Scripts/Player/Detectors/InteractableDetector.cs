using UnityEngine;

/*
 * Subclass of ObjectDetector class.
 * Used to detect Interactable objects with the "Interact" action.
 * 
 * Will call the OnInteraction function on the Interactable object.
 */
[RequireComponent(typeof(CapsuleCollider))]
public class InteractableDetector : ObjectDetector
{
    #region MonoBehaviour
    protected override void Awake()
    {
        base.Awake();
        detectionTag = "Interactable";
    }
    #endregion

    #region Handler
    protected override void InteractClosest(Interactable closest)
    {
        //Debug.Log("[PLAYER INTERACTION] Interacted with " + closest.name);
        closest.OnInteraction();
        UpdateClosestInteractable();
    }
    #endregion
}
