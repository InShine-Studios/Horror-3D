using UnityEngine;

/*
 * Class to control door states and animation.
 * Inherit Interactable.
 */
public class DoorController : Interactable
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


    // General function to change the state of doors
    private void ChangeState()
    {
        //Debug.Log("[INTERACTABLE] " + (isOpen ? "Closing " : "Opening ") + this.name);
        isOpen = !_doorAnim.GetBool(animParam);
        _doorAnim.SetBool(animParam, isOpen);
    }

    public override void OnInteraction()
    {
        //if (inBlocker)
        //{
        //    return;
        //}

        ChangeState();
        Debug.Log("[INTERACTABLE] Door interacted");
        //PlayAudio(isOpen);
    }

    //TODO: Half Open for Ghost Interaction

}
