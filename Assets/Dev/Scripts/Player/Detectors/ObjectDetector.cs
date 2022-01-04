using UnityEngine;
using UnityEngine.InputSystem;

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
    [Header("Input System")]
    protected PlayerInput playerInput;

    [Tooltip("Tag to detect from feet")]
    protected string detectionTag;

    #endregion

    protected abstract void InteractClosest(Interactable closest);

    protected abstract Collider[] FindOverlaps();

    protected Interactable GetClosestInteractable(Collider[] colliders)
    {
        float minDist = Mathf.Infinity;
        Interactable closest = null;

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider cur = colliders[i];

            // Calculate minimum distance and update closest interactable
            float curDist = Utils.GeometryCalcu.GetDistance3D(transform, cur.transform);
            if (cur.CompareTag(detectionTag) && curDist < minDist)
            {
                //Debug.Log("[INTERACTABLE] Updated closest interactable to " + cur.name);
                minDist = curDist;
                closest = cur.GetComponent<Interactable>();
            }
        }

        return closest;
    }

    public void CheckInteraction(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            //Debug.Log("[INTERACTABLE] Player pressed " + ctx.action.name);

            // Find objects that overlap with collider
            Collider[] colliders = this.FindOverlaps();

            Interactable closest = GetClosestInteractable(colliders);

            if (closest)
            {
                InteractClosest(closest);
            }
        }
    }
}
