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

    protected abstract void InteractClosest(Interactable closest);
    protected virtual void Start()
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

    #region Enable - Disable
    private void OnEnable()
    {
        PlayerMovement.FindClosest += SetClosestInteractable;
    }

    private void OnDisable()
    {
        PlayerMovement.FindClosest -= SetClosestInteractable;
    }
    #endregion

    private void SetClosestInteractable()
    {
        Interactable newClosest = GetClosestInteractable();
        if (!newClosest.Equals(closestInteractable))
        {
            closestInteractable.ShowGuideIcon(false);
            closestInteractable = newClosest;
            closestInteractable.ShowGuideIcon(true);
            //Debug.Log("[INTERACTABLE] Updated closest interactable to " + closestInteractable.name);
        }
    }

    protected Interactable GetClosestInteractable()
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

        return newClosest;
    }

    public void CheckInteraction()
    {

        //Debug.Log("[INTERACTABLE] Player pressed");
        if (closestInteractable)
        {
            InteractClosest(closestInteractable);
        }
    }
}
