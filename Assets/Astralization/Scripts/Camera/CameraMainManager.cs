using Astralization.Managers;
using Astralization.Utils.Helper;
using Astralization.Utils.Helper.Inspector;
using UnityEngine;

namespace Astralization.CameraSystem
{
    public class CameraMainManager : MonoBehaviour
    {
        #region Variable
        [SerializeField]
        private SerializableDictionary<string, ICameraManager> _cameraManagers = new SerializableDictionary<string, ICameraManager>();
        #endregion

        #region SetGet
        private string GetCameraName(PlayerHelper.States state)
        {
            return state.ToString() + "Camera"; //ALL CAMERA SHOULD FOLLOW THIS NAMING FORMAT
        }
        #endregion

        #region MonoBehaviour
        private void Awake()
        {
            ICameraManager[] cameraManagers = GetComponentsInChildren<ICameraManager>();
            foreach (ICameraManager cameraManager in cameraManagers)
            {
                _cameraManagers.Add(cameraManager.GetName(), cameraManager);
            }
        }

        private void OnEnable()
        {
            GameManager.CameraStateEvent += ActivateCinemachineBrain;
        }

        private void OnDisable()
        {
            GameManager.CameraStateEvent -= ActivateCinemachineBrain;
        }
        #endregion

        #region CameraManagement
        public void DeactivateAllCinemachineBrain()
        {
            foreach (ICameraManager cameraManager in _cameraManagers.Values)
            {
                cameraManager.Enable(false);
            }
        }

        public void ActivateCinemachineBrain(PlayerHelper.States state)
        {
            DeactivateAllCinemachineBrain();
            _cameraManagers[GetCameraName(state)].Enable(true);
        }
        #endregion
    }
}