using UnityEngine;

public interface IDoorController: IInteractable
{
    bool GetState();
    float GetAngle();
}

/*
 * Class to control door states and animation.
 * Inherit Interactable.
 */
public class DoorController : Interactable, IDoorController
{
    #region Variables
    private const string animParam = "isOpen";
    [Header("Door States")]
    [SerializeField]
    [Tooltip("True if door is in open state")]
    private bool isOpen = false;

    [Space]
    [SerializeField]
    [Header("Animation")]
    private Animator _doorAnim;
    #endregion

    public bool GetState()
    {
        return isOpen;
    }

    public float GetAngle()
    {
        return transform.parent.rotation.y;
    }

    // General function to change the state of doors
    private void ChangeState()
    {
        //Debug.Log("[INTERACTABLE] " + (isOpen ? "Closing " : "Opening ") + this.name);
        isOpen = !_doorAnim.GetBool(animParam);
        _doorAnim.SetBool(animParam, isOpen);
    }

    public override void OnInteraction()
    {

        ChangeState();
        Debug.Log("[INTERACTABLE] Door interacted");
        //PlayAudio(isOpen);
    }

    //TODO: Half Open for Ghost Interaction

}
