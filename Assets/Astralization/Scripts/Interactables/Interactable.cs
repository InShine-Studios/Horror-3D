using Astralization.AudioSystem;
using Astralization.UI.Marker;
using System;
using UnityEngine;

namespace Astralization.Interactables
{
    public interface IInteractable
    {
        void OnInteraction();
        void SetCollider(bool state);
        void SetUseIcon(bool useIcon);
        void ShowMarker(bool isVisible);
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
        [Tooltip("True if marker is used")]
        [SerializeField]
        private bool _useMarker;

        [Tooltip("The animator of marker for guidance")]
        [SerializeField]
        private RuntimeAnimatorController _markerAnimatorController;
        private InteractableItemMarker _marker;

        [Space]
        [Header("Audio")]
        [Tooltip("Audio Manager")]
        private AudioPlayer _audioPlayerObj;
        #endregion

        #region SetGet
        public void ShowMarker(bool isVisible)
        {
            _marker.SetEnabled(isVisible);
        }

        // Function to set Collider state
        public void SetCollider(bool state)
        {
            //Debug.Log("[INTERACTABLE] Setting collider " + this.name + " to " + state);
            GetComponent<Collider>().enabled = state;
        }

        // Function to set Collider state
        public void SetUseIcon(bool useIcon)
        {
            _useMarker = useIcon;
        }
        #endregion

        #region MonoBehaviour
        protected virtual void Awake()
        {
            _audioPlayerObj = GetComponentInChildren<AudioPlayer>();
            _marker = GetComponentInChildren<InteractableItemMarker>();
            _marker.SetAnimator(_markerAnimatorController);
        }

        private void Reset()
        {
            GetComponent<Collider>().isTrigger = true;
        }
        #endregion

        #region Handler
        public abstract void OnInteraction();
        protected void PlayAudio(string name)
        {
            _audioPlayerObj.Play(name);
        }
        #endregion
    }
}