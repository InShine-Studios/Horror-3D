using UnityEngine;

public interface IInteractable
{
    void OnInteraction();
    void SetCollider(bool state);
    void SetInteractableIcon(bool state);
}

/*
 * Interactable abstract class.
 * Parent class for all interactable objects.
 * Implement OnInteraction() function on each child class.
 */
[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour, IInteractable
{
    #region Variables
    [Header("Interactable Icons")]
    [Tooltip("True if there is an icon to be used")]
    public bool hasIcon;

    [Tooltip("The Game Object for the icon")]
    public GameObject interactableIcon;

    [Tooltip("True if player is in Blocker")]
    public bool inBlocker = false;

    [Space]
    [Header("Audio")]
    [Tooltip("Audio Manager")]
    public AudioManager audioManager;

    #endregion

    private void Reset()
    {
        audioManager = GetComponent<AudioManager>();
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
            interactableIcon.SetActive(state);
        }
    }

    // Function to set Collider state
    public void SetCollider(bool state)
    {
        //Debug.Log("[INTERACTABLE] Setting collider " + this.name + " to " + state);
        GetComponent<Collider>().enabled = state;
    }

    protected void PlayAudio(string name)
    {
        audioManager.Play(name);
    }
}
