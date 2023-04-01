using Astralization.AudioSystem;
using Astralization.States.TimeslotStates;
using Astralization.States.WorldStates;
using Astralization.UI.Marker;
using UnityEngine;
using Astralization.Utils.Helper;

namespace Astralization.Items
{
    public interface IItem
    {
        int LogoState { get; }
        ItemHelper.UseBehaviourType UseBehaviourType { get; }
        ItemHelper.WorldConditionType WorldConditionType { get; }

        void Discard();
        RuntimeAnimatorController GetHudLogoAnimatorController();
        void Pick();
        void SetCollider(bool state);
        void SetUseMarker(bool useMarker);
        void ShowItem(bool isShown);
        void ShowMarker(bool state);
        void UpdateMarker(bool updateAnimator = false);
        bool Use();
    }

    /*
     * Item abstract class.
     * Parent class for all item objects.
     * Implement Use() function on each child class.
     */
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class Item : MonoBehaviour, IItem
    {
        #region Variables
        [Header("Item UI")]
        [SerializeField]
        [Tooltip("The item logo animation controller for HUD")]
        protected RuntimeAnimatorController logoAnimatorController;
        public int LogoState { get; protected set; }
        private GameObject _model;


        // Determine item usage behavior
        public ItemHelper.UseBehaviourType UseBehaviourType { get; protected set; }
        // Determine world condition type so the item can be used
        public ItemHelper.WorldConditionType WorldConditionType { get; protected set; }

        [Space]
        [Header("Audio")]
        [Tooltip("Audio Manager")]
        private AudioPlayer _audioPlayerObj;

        [Header("Item Icons")]
        [Tooltip("True if there is an icon to be used")]
        [SerializeField]
        private bool _useMarker;

        [Tooltip("The icon mark for guidance")]
        private InteractableItemMarker _marker;

        #endregion

        #region SetGet
        private void SetMeshRenderer(bool enabled)
        {
            _model.SetActive(enabled);
        }

        public void ShowItem(bool isShown)
        {
            //Debug.Log("[ITEM] Show " + this.name + " visibility to:" + isShown);
            gameObject.SetActive(isShown);
        }

        public RuntimeAnimatorController GetHudLogoAnimatorController()
        {
            return logoAnimatorController;
        }

        public void SetCollider(bool state)
        {
            //Debug.Log("[INTERACTABLE] Setting collider " + this.name + " to " + state);
            GetComponent<Collider>().enabled = state;
        }

        public void ShowMarker(bool state)
        {
            if (_useMarker)
            {
                //Debug.Log("[INTERACTABLE] Setting icon " + this.name + " to " + state);
                _marker.SetEnabled(state);
            }
        }

        public void SetUseMarker(bool useMarker)
        {
            _useMarker = useMarker;
        }
        #endregion

        #region Monobehavior
        protected virtual void Awake()
        {
            _audioPlayerObj = GetComponentInChildren<AudioPlayer>();
            _model = transform.Find("Model").gameObject;
            _marker = GetComponentInChildren<InteractableItemMarker>();
            UpdateMarker(updateAnimator: true);
        }
        #endregion

        #region Use
        protected bool IsUsableOnCurrentWorld()
        {
            IWorldState currentWorldState = (IWorldState)WorldStateMachine.Instance.CurrentState;
            if (currentWorldState is IWorldRealState && WorldConditionType.HasFlag(ItemHelper.WorldConditionType.Real))
            {
                return true;
            }
            else if (currentWorldState is IWorldAstralState && WorldConditionType.HasFlag(ItemHelper.WorldConditionType.Astral))
            {
                return true;
            }
            else if (WorldStateMachine.Instance.IsKillPhase() && WorldConditionType.HasFlag(ItemHelper.WorldConditionType.KillPhase))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Use()
        {
            if (!IsUsableOnCurrentWorld())
            {
                return false;
            }

            ActivateFunctionality();
            return true;
        }
        protected abstract void ActivateFunctionality();
        #endregion

        #region Handler
        protected void PlayAudio(string name)
        {
            _audioPlayerObj.Play(name);
        }
        #endregion

        #region Pick - Discard
        public virtual void Pick()
        {
            SetCollider(false);
            SetMeshRenderer(false);
            ShowMarker(false);
            SetUseMarker(false);
        }

        public void Discard()
        {
            SetCollider(true);
            SetMeshRenderer(true);
            SetUseMarker(true);
        }
        #endregion

        #region MarkerHandler
        public void UpdateMarker(bool updateAnimator = false)
        {
            if (updateAnimator)
            {
                _marker.SetAnimator(logoAnimatorController, LogoState);
            }
            else
            {
                _marker.SetAnimation(LogoState);
            }
        }
        #endregion
    }
}