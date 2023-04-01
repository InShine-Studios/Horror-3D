using Cinemachine;
using UnityEngine;

namespace Astralization.CameraSystem
{
    public class DefaultCameraManager : MonoBehaviour, ICameraManager
    {
        #region Variables
        private Camera _camera;
        private CinemachineBrain _cinemachineBrain;
        private CinemachineVirtualCamera _hidingCamera;
        private CinemachineFreeLook _freeLookCamera;
        #endregion

        #region SetGet
        public void Enable(bool enable)
        {
            _camera.enabled = enable;
            _cinemachineBrain.enabled = enable;
            _freeLookCamera.enabled = enable;
        }

        public string GetName()
        {
            return name;
        }
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _cinemachineBrain = GetComponent<CinemachineBrain>();
            _hidingCamera = GetComponentInChildren<CinemachineVirtualCamera>();
            _freeLookCamera = GetComponentInChildren<CinemachineFreeLook>();
        }
        #endregion
    }
}