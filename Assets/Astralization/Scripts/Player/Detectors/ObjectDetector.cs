using System.Collections.Generic;
using UnityEngine;

/*
 * Class to detect objects close to player.
 * Takes InputSystem from parent Player.
 * 
 * Finds objects using shape overlap.
 */
[RequireComponent(typeof(Collider))]
public abstract class ObjectDetector : MonoBehaviour
{
    #region Variables
    [Header("Dependant on Detectors")]
    [Tooltip("Tag to distinguish interactable types")]
    protected string detectionTag;

    [Tooltip("Current closest object to this detector")]
    protected List<Interactable> nearbyInteractables;

    [Tooltip("Current closest object to this detector")]
    protected Interactable closestInteractable;
    #endregion

    #region SetGet
    public Interactable GetClosest()
    {
        return closestInteractable;
    }
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        nearbyInteractables = new List<Interactable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(detectionTag))
        {
            Interactable interactable = other.GetComponent<Interactable>();
            // Add interactable to list
            nearbyInteractables.Add(interactable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(detectionTag))
        {
            Interactable interactable = other.GetComponent<Interactable>();
            // Remove interactable from list
            nearbyInteractables.Remove(interactable);

            if (interactable.Equals(closestInteractable))
            {
                // Turn off icon
                closestInteractable.ShowGuideIcon(false);
                closestInteractable = null;
            }
        }
    }

    private void OnEnable()
    {
        PlayerMovement.FindClosest += UpdateClosestInteractable;
    }

    private void OnDisable()
    {
        PlayerMovement.FindClosest -= UpdateClosestInteractable;
    }
    #endregion

    #region Handler
    protected void UpdateClosestInteractable()
    {
        float minDist = Mathf.Infinity;
        Interactable newClosest = null;

        for (int i = 0; i < nearbyInteractables.Count; i++)
        {
            Interactable cur = nearbyInteractables[i];

            // Calculate minimum distance and update closest interactable
            float curDist = Utils.GeometryCalcu.GetDistance3D(transform.position, cur.transform.position);
            if (curDist < minDist)
            {
                minDist = curDist;
                newClosest = cur;
            }
        }

        if (closestInteractable && !newClosest.Equals(closestInteractable))
        {
            closestInteractable.ShowGuideIcon(false);
            //Debug.Log("[PLAYER INTERACTION] Updated closest interactable to " + closestInteractable.name);
        }
        closestInteractable = newClosest; // newClosest can be null when no nearby
        closestInteractable?.ShowGuideIcon(true);
    }

    public void CheckInteraction()
    {
        //Debug.Log("[PLAYER INTERACTION] Initiate interaction");
        if (closestInteractable)
        {
            InteractClosest(closestInteractable);
        }
    }

    protected abstract void InteractClosest(Interactable closest);
    #endregion
}
