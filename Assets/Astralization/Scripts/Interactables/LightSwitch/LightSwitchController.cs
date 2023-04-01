using System;
using UnityEngine;

namespace Astralization.Interactables.LightSwitch
{
    public interface ILightSwitchController : IInteractable
    {
        bool GetState();
    }

    /*
     * Class to control light switch states and sprite.
     * Inherit Interactable.
     */
    public class LightSwitchController : Interactable, ILightSwitchController
    {
        #region Constants
        private const string _animParam = "isOn";
        #endregion

        #region Variables
        [Header("Light switch state")]
        [SerializeField]
        [Tooltip("True if light switch is on")]
        private bool _isOn = false;

        [Space]
        [Header("Light")]
        [Tooltip("Light Source")]
        private Light[] _lightSources;

        [Space]
        [SerializeField]
        [Header("Animation")]
        private Animator _animator;
        #endregion

        #region SetGet
        private void SetLightSources(bool value)
        {
            foreach (Light lightSource in _lightSources)
            {
                lightSource.enabled = value;
            }
        }

        private void ChangeState()
        {
            //Debug.Log("[INTERACTABLE] " + (isOpen ? "Closing " : "Opening ") + this.name);
            _isOn = !_isOn;
            _animator.SetBool(_animParam, _isOn);
        }

        public bool GetState()
        {
            return _isOn;
        }
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            _lightSources = GetComponentsInChildren<Light>();
        }
        #endregion

        #region Handler
        public override void OnInteraction()
        {
            //Debug.Log(
            //    String.Format("[INTERACTABLE] Turning \"{0}\" {1}", this.name, (isOn ? "on" : "off"))
            //);
            ChangeState();
            SetLightSources(_isOn);
            if (_isOn) PlayAudio("Switch_On");
            else PlayAudio("Switch_Off");
        }
        #endregion
    }
}