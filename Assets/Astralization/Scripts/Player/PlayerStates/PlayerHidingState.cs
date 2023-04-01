using Astralization.CameraSystem;
using Astralization.Interactables.Closet;
using Astralization.Player.Detectors;
using Astralization.Utils.Helper;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Astralization.Player.PlayerStates
{

    /*
     * Class to manage hiding state
     */
    public class PlayerHidingState : PlayerState
    {
        #region Events
        public static event Action<bool> StopHidingHudEvent;
        #endregion

        #region Variables
        [Tooltip("Closets interactable that been interacted by player")]
        private Transform _closets;
        [Tooltip("PlayerMovement script that gonna be toogled")]
        private PlayerMovement _playerMovement;
        [Tooltip("InteractableDetector script to get closets")]
        private InteractableDetector _interactableDetector;
        [Tooltip("Player previous position")]
        private Vector3 _prevPosition;
        private HidingCameraConfigs _hidingCameraConfigs;
        private Transform _closetsPoint;
        private Cinemachine.CinemachineFreeLook _freelook;
        private Cinemachine.CinemachineVirtualCamera _vcam;
        private CinemachinePOVExtension _cinemachinePOVExtension;
        #endregion

        #region MonoBehaviour
        protected override void Awake()
        {
            base.Awake();
            _cinemachinePOVExtension = transform.parent.GetComponentInChildren<CinemachinePOVExtension>();
            _playerMovement = GetComponent<PlayerMovement>();
            _interactableDetector = GetComponentInChildren<InteractableDetector>();
            _vcam = transform.parent.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
            _freelook = transform.parent.GetComponentInChildren<Cinemachine.CinemachineFreeLook>();
        }
        #endregion

        #region StateHandler
        public override void Enter()
        {
            base.Enter();
            PlayerMovementChangeState();

            _closets = _interactableDetector.GetClosest().transform;
            _hidingCameraConfigs = _closets.GetComponent<HidingCameraConfigs>();
            _closetsPoint = new GameObject().transform;
            _closetsPoint.parent = _closets;

            _closetsPoint.localPosition = _hidingCameraConfigs.StartingPosition;
            _cinemachinePOVExtension.SetClosetCameraSetting(_hidingCameraConfigs);

            _prevPosition = transform.position;
            Vector3 calOffset = _closets.GetComponentInChildren<Renderer>().bounds.center;
            transform.position = new Vector3(calOffset.x, 0, calOffset.z);

            _freelook.enabled = false;
            _vcam.enabled = true;
            _vcam.m_Follow = _closetsPoint;
        }

        public override void Exit()
        {
            base.Exit();
            transform.position = _prevPosition;

            _vcam.m_Follow = null;
            _freelook.enabled = true;
            _vcam.enabled = false;
            _closets = null;

            Invoke("PlayerMovementChangeState", 0.1f);
        }

        private void PlayerMovementChangeState()
        {
            _playerMovement.enabled = !_playerMovement.enabled;
        }
        #endregion

        #region InputHandler
        public override void UnhidePlayer(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                owner.SetPlayerActionMap(PlayerHelper.States.Default);
            }
        }

        public override void OnMouseDelta(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                _cinemachinePOVExtension.SetMouseDeltaInput(ctx.ReadValue<Vector2>());
            }
        }
        #endregion
    }
}