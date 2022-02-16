using System;
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
    public bool UseIcon;

    [Tooltip("The Game Object for the icon")]
    [SerializeField]
    private GameObject _InteractableIcon;

    [Space]
    [Header("Audio")]
    [Tooltip("Audio Manager")]
    protected AudioPlayer AudioPlayerObj;

    #endregion

    protected virtual void Awake()
    {
        AudioPlayerObj = GetComponentInChildren<AudioPlayer>();
    }

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    public abstract void OnInteraction();

    protected void OnTriggerExit(Collider collision)
    {
        if (Utils.PlayerHelper.CheckIsInteractZone(collision))
        {
            SetInteractableIcon(false);
        }
    }

    // Function to set Icon state
    public void SetInteractableIcon(bool state)
    {
        if (UseIcon)
        {
            //Debug.Log("[INTERACTABLE] Setting icon " + this.name + " to " + state);
            _InteractableIcon.SetActive(state);
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
        AudioPlayerObj.Play(name);
    }
}
