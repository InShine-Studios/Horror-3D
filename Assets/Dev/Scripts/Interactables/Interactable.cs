using UnityEngine;

/*
 * Interactable abstract class.
 * Parent class for all interactable objects.
 * Implement OnInteraction() function on each child class.
 */
[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    #region Variables
    [Header("Interactable Icons")]
    [Tooltip("True if there is an icon to be used")]
    public bool hasIcon;

    [Tooltip("The Sprite Renderer for the icon")]
    public SpriteRenderer interactableIcon;

    [Tooltip("True if player is in Blocker")]
    public bool inBlocker = false;

    [Space]
    [Header("Audio")]
    [Tooltip("Audio for Turning On")]
    public AudioSource audioOn;
    [Tooltip("Audio for Turning Off")]
    public AudioSource audioOff;

    #endregion

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public abstract void OnInteraction();

    private void OnTriggerEnter(Collider collision)
    {
        if (Utils.PlayerHelper.CheckIsInteractZone(collision))
        {
            SetInteractableIcon(true);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (Utils.PlayerHelper.CheckIsInteractZone(collision))
        {
            SetInteractableIcon(false);
        }
    }

    // Function to set Icon state
    public void SetInteractableIcon(bool state)
    {
        if (hasIcon)
        {
            //Debug.Log("[INTERACTABLE] Setting icon " + this.name + " to " + state);
            interactableIcon.enabled = state;
        }
    }

    // Function to set Collider state
    public void SetCollider(bool state)
    {
        //Debug.Log("[INTERACTABLE] Setting collider " + this.name + " to " + state);
        GetComponent<Collider>().enabled = state;
    }

    protected void PlayAudio(bool isOn)
    {
        if (isOn)
        {
            audioOn.Play();
        }
        else
        {
            audioOff.Play();
        }
    }
}
