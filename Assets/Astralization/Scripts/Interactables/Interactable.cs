using System;
using UnityEngine;

public interface IInteractable
{
    void OnInteraction();
    void SetCollider(bool state);
    void ShowGuideIcon(bool state);
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
    [SerializeField]
    private bool _useIcon;

    [Tooltip("The Game Object for the icon")]
    [SerializeField]
    private GameObject _interactableIcon;

    [Space]
    [Header("Audio")]
    [Tooltip("Audio Manager")]
    protected AudioPlayer AudioPlayerObj;
    #endregion

    #region SetGet
    public void ShowGuideIcon(bool state)
    {
        if (_useIcon)
        {
            //Debug.Log("[INTERACTABLE] Setting icon " + this.name + " to " + state);
            _interactableIcon.SetActive(state);
        }
    }

    // Function to set Collider state
    public void SetCollider(bool state)
    {
        //Debug.Log("[INTERACTABLE] Setting collider " + this.name + " to " + state);
        GetComponent<Collider>().enabled = state;
    }
    #endregion

    #region MonoBehaviour
    protected virtual void Awake()
    {
        AudioPlayerObj = GetComponentInChildren<AudioPlayer>();
    }

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }
    #endregion

    public abstract void OnInteraction();

    protected void PlayAudio(string name)
    {
        AudioPlayerObj.Play(name);
    }
}
